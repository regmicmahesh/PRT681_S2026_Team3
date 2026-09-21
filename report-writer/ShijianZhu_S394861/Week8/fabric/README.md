# Week 4 Fabric package

1. Create a Fabric Lakehouse and upload the contents of `data/` and `week4_spatial/` to `Files/week4/`.
2. Import and run `week4_lakehouse_ingestion.ipynb`.
3. Create a Direct Lake semantic model from the three Delta tables.
4. Relate crime to geography on Statistical Area 2 / SA2 Name and add a Date table.
5. Add the measures in `direct_lake_measures.dax`, then connect the supplied Power BI report design.

The local PBIP uses Import mode because Direct Lake requires a Fabric workspace. The included notebook and DAX file reproduce the model in Fabric.
