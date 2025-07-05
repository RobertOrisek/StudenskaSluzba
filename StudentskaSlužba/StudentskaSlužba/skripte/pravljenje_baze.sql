Create Database studentska_sluzba;

use studentska_sluzba;

create table nastavnici(
	id int primary key identity,
	ime nvarchar(20),
	prezime nvarchar(20),
	zvanje nvarchar(20)
);

create table predmeti(
	id int primary key identity,
	naziv nvarchar(20)
);

create table studenti(
	id int primary key identity,
	studentski_index nvarchar(20),
	ime nvarchar(20),
	prezime nvarchar(20),
	grad nvarchar(20)
);

create table ispitni_rokovi(
	id int primary key identity,
	naziv nvarchar(20),
	pocetak date,
	kraj date
);

create table ispitne_prijave(
	student_id int foreign key references studenti (id),
	predmet_id int foreign key references predmeti (id),
	ispitni_rok_id int foreign key references ispitni_rokovi (id),
	teorija int,
	zadaci int
);

create table pradje(
	nastavnik_id int foreign key references nastavnici (id),
	predmet_id int foreign key references predmeti (id),
);

create table pohadja(
	student_id int foreign key references studenti (id),
	predmet_id int foreign key references predmeti (id)
);