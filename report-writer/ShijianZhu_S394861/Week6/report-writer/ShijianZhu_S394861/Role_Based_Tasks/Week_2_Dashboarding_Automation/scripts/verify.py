"""Cross-file checks; this does not execute Power BI, DAX or VBA."""
from pathlib import Path
import base64, csv, json, re, zipfile
import xml.etree.ElementTree as ET
R=Path(__file__).resolve().parents[1]
wide=list(csv.DictReader((R/'data/parks_source_wide.csv').open()))
rows=list(csv.DictReader((R/'data/park_visits.csv').open()))
for r in rows:r['Year']=int(r['Year']);r['Visits']=int(r['Visits'])
assert len(wide)==22 and len(rows)==110
assert len({(r['Year'],r['Park']) for r in rows})==110
bypark={r['Park']:r for r in wide}
for r in rows:
 assert r['Visits']==int(bypark[r['Park']][str(r['Year'])])
 assert r['Region']==bypark[r['Park']]['Region']
 assert r['Visits']>=0 and r['Visits']%100==0
expected={2021:3341400,2022:3584600,2023:3453500,2024:3189600,2025:3359700}
for y,total in expected.items():assert sum(r['Visits'] for r in rows if r['Year']==y)==total
assert sum(r['Visits'] for r in rows if r['Year']==2025 and r['Region']=='Darwin')==2103500
assert sum(r['Visits'] for r in rows if r['Year']==2024 and r['Region']=='Katherine')==532800
m=(R/'powerbi/Visits.m').read_text()
b64=re.search(r'Binary.FromText\("([^"]+)"',m).group(1)
assert base64.b64decode(b64)==(R/'data/park_visits.csv').read_bytes()
model=json.loads((R/'powerbi/NTParkVisits.SemanticModel/model.bim').read_text())['model']
tables={t['name']:t for t in model['tables']}
assert set(tables)=={'Year','Park','Visits'}
assert len(tables['Visits']['measures'])==6
assert '\n'.join(tables['Visits']['partitions'][0]['source']['expression'])==m.rstrip('\n')
for rel in model['relationships']:
 for side in ['from','to']:
  assert rel[side+'Column'] in [c['name'] for c in tables[rel[side+'Table']]['columns']]
 assert rel['fromCardinality']=='many' and rel['toCardinality']=='one'
assert len(model['relationships'])==2
page=R/'powerbi/NTParkVisits.Report/definition/pages/Overview'
page_def=json.loads((page/'page.json').read_text())
assert {'source':'yearFilter','target':'annualTrend','type':'NoFilter'} in page_def['visualInteractions']
visuals=list(page.glob('visuals/*/visual.json'))
assert len(visuals)==8
for f in visuals:
 v=json.loads(f.read_text());p=v['position']
 assert 0<=p['x'] and p['x']+p['width']<=page_def['width']
 assert 0<=p['y'] and p['y']+p['height']<=page_def['height']
 for role in v['visual']['query']['queryState'].values():
  for projection in role['projections']:
   kind,value=next(iter(projection['field'].items()))
   t=tables[value['Expression']['SourceRef']['Entity']]
   names=[c['name'] for c in t['measures' if kind=='Measure' else 'columns']]
   assert value['Property'] in names
html=(R/'preview/dashboard.html').read_text()
assert json.loads(re.search(r'<script id="dataset" type="application/json">(.*?)</script>',html,re.S).group(1))==rows
assert json.loads((R/'data/preview_data.json').read_text())==rows
ns={'s':'http://schemas.openxmlformats.org/spreadsheetml/2006/main'}
with zipfile.ZipFile(R/'excel/NT_Park_Visits.xlsx') as z:
 sheets=[n for n in z.namelist() if re.fullmatch(r'xl/worksheets/sheet\d+.xml',n)]
 assert len(sheets)==5
 for name in sheets:
  root=ET.fromstring(z.read(name))
  assert not root.findall('.//s:c[@t="e"]',ns),name
 report=ET.fromstring(z.read('xl/worksheets/sheet1.xml'))
 cells={c.attrib['r']:c for c in report.findall('.//s:c',ns)}
 for cell,value in {'D7':3359700,'F7':3189600,'H7':3359700/3189600-1}.items():
  actual=float(cells[cell].find('s:v',ns).text)
  assert abs(actual-value)<1e-9,(cell,actual,value)
 charts=[n for n in z.namelist() if re.fullmatch(r'xl/(?:drawings/)?charts/chart\d+\.xml',n)]
 assert charts
 assert any(b'lineChart' in z.read(n) for n in charts)
print('PASS: 110 values, 5 official totals, embedded CSV, model bindings, 8 visual bounds, preview dataset, 5 XLSX sheets and 3 cached KPI formulas.')
print('Power BI Desktop, DAX engine, VBA and actual browser rendering are not exercised by this script.')
