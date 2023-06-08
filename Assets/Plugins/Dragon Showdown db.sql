BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Jugador" (
	"Nombre"	TEXT NOT NULL,
	"Dinero"	INTEGER NOT NULL,
	"Id_Monstruo1"	INTEGER,
	"Id_Monstruo2"	INTEGER,
	"Id_Monstruo3"	INTEGER,
	FOREIGN KEY("Id_Monstruo1") REFERENCES "Monstruo"("Id"),
	FOREIGN KEY("Id_Monstruo3") REFERENCES "Monstruo"("Id"),
	FOREIGN KEY("Id_Monstruo2") REFERENCES "Monstruo"("Id")
);
CREATE TABLE IF NOT EXISTS "Ataque" (
	"Id"	INTEGER,
	"Nombre"	TEXT NOT NULL,
	"Descripcion"	TEXT,
	"Potencia"	INTEGER NOT NULL,
	"Precision"	INTEGER NOT NULL,
	PRIMARY KEY("Id")
);
CREATE TABLE IF NOT EXISTS "Monstruo" (
	"Id"	INTEGER,
	"Nombre"	TEXT NOT NULL,
	"HP"	INTEGER NOT NULL,
	"Ataque"	INTEGER NOT NULL,
	"Defensa"	TEXT NOT NULL,
	"Velocidad"	INTEGER NOT NULL,
	"Inteligencia"	INTEGER NOT NULL,
	"Cordura"	INTEGER NOT NULL,
	"Atributo"	TEXT NOT NULL,
	"Elemento"	TEXT NOT NULL,
	"Id_AtaqueFisico"	INTEGER,
	"Id_AtaqueEstado"	INTEGER,
	"Id_AtaqueElemental"	INTEGER,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("Id_AtaqueEstado") REFERENCES "Ataque"("Id"),
	FOREIGN KEY("Id_AtaqueFisico") REFERENCES "Ataque"("Id"),
	FOREIGN KEY("Id_AtaqueElemental") REFERENCES "Ataque"("Id")
);
COMMIT;
