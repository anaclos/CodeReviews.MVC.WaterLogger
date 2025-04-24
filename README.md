# MVC.WaterLogger
## Description
This program allows you to store the water you drink at a given time in a database.
## Database
It uses a SQLite database named WaterLogger.db, which has not been uploaded to GitHub.

The connection string name is found in the appsettings.json file, where you can modify the database name.
## Table
To create the drinking_water table, you can use this script or do it manually.

CREATE TABLE "drinking_water" (
"Id" INTEGER,
"Date" TEXT,
"Unit" TEXT,
"Quantity" INTEGER,
PRIMARY KEY("Id" AUTOINCREMENT)
)
## Enhancements
Added Unit to reference the type of unit of measurement used (Glass, Bottle, Big Bottle)
