CREATE PROCEDURE pGetThreeClosestBranches (@lat float, @lng float)
AS
SELECT TOP 3 Id, Street, City, Region, Latitude,
Longitude, SQRT(POWER(Latitude - @lat, 2) + POWER(Longitude - @lng, 2)) *
1000 AS Distance -- km calculation
FROM Stores
ORDER BY Distance