
set destination="C:\Users\NMiladin\OneDrive - Quest\Documents\Diplomski\lucene-solr\solr\server\solr\MuzickiPojam\lyrics"

FOR %%G IN (B,C,D,E,F,G,H,I) DO (
	@xcopy /Y /R %%G %destination%
)

pause