"""Build data and Power BI files from the checked NT Government transcription."""
from pathlib import Path
import base64, csv, io, json
ROOT=Path(__file__).resolve().parents[1]
SOURCE='https://dth.nt.gov.au/parks-and-wildlife/parks-and-wildlife-statistics-and-research/park-visitor-data'
SCHEMA='https://developer.microsoft.com/json-schemas/fabric/item/'
def write(path,text):
    f=ROOT/path; f.parent.mkdir(parents=True,exist_ok=True); f.write_text(text,encoding='utf-8')
def js(path,value):write(path,json.dumps(value,indent=2)+'\n')
wide=list(csv.DictReader((ROOT/'data/parks_source_wide.csv').open()))
years=list(range(2021,2026))
official={2021:3341400,2022:3584600,2023:3453500,2024:3189600,2025:3359700}
assert len(wide)==22
for year in years:assert sum(int(r[str(year)]) for r in wide)==official[year]
rows=[{'Year':year,'Region':r['Region'],'Park':r['Park'],'Visits':int(r[str(year)])} for r in wide for year in years]
out=io.StringIO();writer=csv.DictWriter(out,fieldnames=['Year','Region','Park','Visits'],lineterminator='\n');writer.writeheader();writer.writerows(rows)
text=out.getvalue();write('data/park_visits.csv',text)
js('data/source_metadata.json',{'publisher':'Parks & Wildlife NT / Northern Territory Government','title':'Park visitor data','url':SOURCE,'accessed':'2026-09-05','coverage':'22 listed parks/reserves; 2021–2025 calendar years','excluded':'The three entries under Other','unit':'estimated annual visits, rounded to nearest 100','extraction':'Manually transcribed numeric table; punctuation and whole-of-park name capitalisation normalised. No values imputed.','official_totals':official})
js('data/preview_data.json',rows)
measures=[
 ('Total Visits','SUM(Visits[Visits])','#,0'),
 ('Selected Year',"MAX('Year'[Year])",'0'),
 ('Selected Year Visits',"VAR Y = [Selected Year]\nRETURN CALCULATE([Total Visits], REMOVEFILTERS('Year'), 'Year'[Year] = Y)",'#,0'),
 ('Previous Year Visits',"VAR Y = [Selected Year]\nRETURN CALCULATE([Total Visits], REMOVEFILTERS('Year'), 'Year'[Year] = Y - 1)",'#,0'),
 ('YoY Change','DIVIDE([Selected Year Visits] - [Previous Year Visits], [Previous Year Visits])','0.0%;-0.0%;0.0%'),
 ('Visit Change','IF(NOT ISBLANK([Previous Year Visits]), [Selected Year Visits] - [Previous Year Visits])','+#,0;-#,0;0')]
write('powerbi/Measures.dax','// Create each definition as a separate measure in Visits.\n// Annual data: no fabricated month or YTD calculation.\n\n'+'\n\n'.join(n+' =\n'+e for n,e,_ in measures)+'\n')
embedded=base64.b64encode(text.encode()).decode()
query='\n'.join(['let',f'    Source = Csv.Document(Binary.FromText("{embedded}", BinaryEncoding.Base64), [Delimiter=",", Columns=4, Encoding=65001, QuoteStyle=QuoteStyle.Csv]),','    Headers = Table.PromoteHeaders(Source, [PromoteAllScalars=true]),','    Typed = Table.TransformColumnTypes(Headers, {{"Year", Int64.Type}, {"Region", type text}, {"Park", type text}, {"Visits", Int64.Type}})','in','    Typed'])
write('powerbi/Visits.m',query+'\n')
def col(name,typ,**kw):return dict(name=name,dataType=typ,sourceColumn=name,summarizeBy='none',**kw)
def partition(name,expression):return {'name':name,'mode':'import','source':{'type':'m','expression':expression.splitlines()}}
model={'name':'NTParkVisits','compatibilityLevel':1567,'model':{'culture':'en-AU','sourceQueryCulture':'en-AU','defaultPowerBIDataSourceVersion':'powerBI_V3','dataAccessOptions':{'legacyRedirects':True,'returnErrorValuesAsNull':True},'tables':[
 {'name':'Visits','columns':[col('Year','int64',isHidden=True),col('Region','string',isHidden=True),col('Park','string',isHidden=True),col('Visits','int64',formatString='#,0')],'measures':[{'name':n,'expression':e,'formatString':f} for n,e,f in measures],'partitions':[partition('Visits',query)]},
 {'name':'Year','columns':[col('Year','int64',isKey=True,formatString='0')],'partitions':[partition('Year','#table(type table [Year = Int64.Type], {{2021}, {2022}, {2023}, {2024}, {2025}})')]},
 {'name':'Park','columns':[col('Park','string',isKey=True),col('Region','string')],'partitions':[partition('Park','Table.Distinct(Table.SelectColumns(Visits, {"Park", "Region"}))')]}
 ],'relationships':[{'name':'Visits_Year','fromTable':'Visits','fromColumn':'Year','toTable':'Year','toColumn':'Year','fromCardinality':'many','toCardinality':'one','crossFilteringBehavior':'oneDirection'},{'name':'Visits_Park','fromTable':'Visits','fromColumn':'Park','toTable':'Park','toColumn':'Park','fromCardinality':'many','toCardinality':'one','crossFilteringBehavior':'oneDirection'}]}}
js('powerbi/NTParkVisits.SemanticModel/model.bim',model)
js('powerbi/NTParkVisits.SemanticModel/definition.pbism',{'$schema':SCHEMA+'semanticModel/definitionProperties/1.0.0/schema.json','version':'1.0','settings':{}})
js('powerbi/NTParkVisits.pbip',{'version':'1.0','artifacts':[{'report':{'path':'NTParkVisits.Report'}}],'settings':{'enableAutoRecovery':True}})
js('powerbi/NTParkVisits.Report/definition.pbir',{'$schema':SCHEMA+'report/definitionProperties/2.0.0/schema.json','version':'4.0','datasetReference':{'byPath':{'path':'../NTParkVisits.SemanticModel'}}})
base='powerbi/NTParkVisits.Report/definition/'
js(base+'version.json',{'$schema':SCHEMA+'report/definition/versionMetadata/1.0.0/schema.json','version':'2.0.0'})
js(base+'report.json',{'$schema':SCHEMA+'report/definition/report/2.0.0/schema.json','themeCollection':{}})
js(base+'pages/pages.json',{'$schema':SCHEMA+'report/definition/pagesMetadata/1.0.0/schema.json','pageOrder':['Overview'],'activePageName':'Overview'})
js(base+'pages/Overview/page.json',{'$schema':SCHEMA+'report/definition/page/1.0.0/schema.json','name':'Overview','displayName':'NT parks - estimated annual visits','displayOption':'FitToPage','width':1280,'height':850,'visualInteractions':[{'source':'yearFilter','target':'annualTrend','type':'NoFilter'}]})
def field(table,name,measure=False):return {'Measure' if measure else 'Column':{'Expression':{'SourceRef':{'Entity':table}},'Property':name}}
def lit(value):return {'expr':{'Literal':{'Value':value}}}
visuals=[]
def visual(name,typ,title,pos,roles,sort=None,direction='Ascending'):
    state={r:{'projections':[{'field':field(t,n,m),'queryRef':t+'.'+n,'nativeQueryRef':n} for t,n,m in fields]} for r,fields in roles.items()}
    v={'visualType':typ,'query':{'queryState':state},'drillFilterOtherVisuals':True,'visualContainerObjects':{'title':[{'properties':{'show':lit('true'),'text':lit("'"+title+"'")}}]},'objects':{'dataPoint':[{'properties':{'defaultColor':{'solid':{'color':lit("'#444444'")}}}}]}}
    if sort:v['query']['sortDefinition']={'sort':[{'field':field(*sort),'direction':direction}]}
    x,y,w,h=pos
    js(base+f'pages/Overview/visuals/{name}/visual.json',{'$schema':SCHEMA+'report/definition/visualContainer/2.1.0/schema.json','name':name,'position':{'x':x,'y':y,'width':w,'height':h,'z':len(visuals),'tabOrder':len(visuals)},'visual':v});visuals.append(name)
for i,(n,title) in enumerate([('Selected Year Visits','Estimated visits - most recent selected year'),('Previous Year Visits','Previous-year estimated visits'),('YoY Change','Year-on-year change')]):visual('metric'+str(i),'card',title,(20+i*330,20,310,120),{'Values':[('Visits',n,True)]})
visual('yearFilter','slicer','Year - clear selects latest',(1020,20,240,190),{'Values':[('Year','Year',False)]},('Year','Year',False),'Descending')
visual('regionFilter','slicer','Region',(1020,230,240,230),{'Values':[('Park','Region',False)]})
visual('annualTrend','lineChart','Estimated visits - annual trend',(20,160,610,310),{'Category':[('Year','Year',False)],'Y':[('Visits','Total Visits',True)]},('Year','Year',False))
visual('regions','clusteredBarChart','Region comparison - selected year',(650,160,350,310),{'Category':[('Park','Region',False)],'Y':[('Visits','Selected Year Visits',True)]},('Visits','Selected Year Visits',True),'Descending')
visual('parkDetails','tableEx','Park comparison - source estimates',(20,490,1240,340),{'Values':[('Park','Park',False),('Park','Region',False),('Visits','Selected Year Visits',True),('Visits','Previous Year Visits',True),('Visits','Visit Change',True),('Visits','YoY Change',True)]},('Visits','Selected Year Visits',True),'Descending')
js('data/check_totals.json',{'official':official,'regions_2025':{region:sum(r['Visits'] for r in rows if r['Year']==2025 and r['Region']==region) for region in dict.fromkeys(r['Region'] for r in rows)},'yoy_2025':official[2025]/official[2024]-1})
print('Generated 110 park-year records, 6 DAX measures and 8 report visuals. All 5 annual totals reconcile to the official table.')
