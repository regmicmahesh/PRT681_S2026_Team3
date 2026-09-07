// Headless logic check with a DOM stub, not a browser layout test.
import fs from 'node:fs';
import vm from 'node:vm';
import assert from 'node:assert/strict';
const html=fs.readFileSync(new URL('../preview/dashboard.html',import.meta.url),'utf8');
const dataset=html.match(/<script id="dataset" type="application\/json">([\s\S]*?)<\/script>/)[1];
const code=[...html.matchAll(/<script>([\s\S]*?)<\/script>/g)].at(-1)[1];
const elements={};
for(const id of ['dataset','year','region','total','previous','change','yearlabel','delta','trend','regions','parks','unpivot','status']){
 elements[id]={value:'',textContent:'',innerHTML:'',events:{},addEventListener(type,fn){this.events[type]=fn;}};
}
elements.dataset.textContent=dataset;elements.year.value='2025';elements.region.value='All';
vm.runInNewContext(code,{document:{getElementById(){return{querySelector:s=>elements[s.slice(1)]};}}});
assert.equal(elements.total.textContent,'3,359,700');
assert.equal(elements.previous.textContent,'3,189,600');
assert.equal(elements.change.textContent,'+5.3%');
assert.equal((elements.parks.innerHTML.match(/<tr>/g)||[]).length,22);
elements.region.value='Darwin';elements.region.events.change();
assert.equal(elements.total.textContent,'2,103,500');
assert.equal(elements.previous.textContent,'1,990,400');
assert.equal((elements.parks.innerHTML.match(/<tr>/g)||[]).length,6);
elements.year.value='2021';elements.year.events.change();
assert.equal(elements.previous.textContent,'N/A');assert.equal(elements.change.textContent,'N/A');
assert.equal((elements.trend.innerHTML.match(/class="barrow"/g)||[]).length,5);
elements.region.value='All';elements.region.events.change();
assert.equal(elements.total.textContent,'3,341,400');
elements.unpivot.events.click();assert.match(elements.status.textContent,/Simulation: 110 records/);
console.log('PASS: initial KPIs, region and year selection, park row counts, missing prior year, full trend and labelled automation simulation.');
