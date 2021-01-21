
set destination="D:\lucene-solr\solr\server\solr\MuzickiPojam\lyrics"

FOR %%G IN (A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z) DO (
	@xcopy /Y /R %%G %destination%
)

pause