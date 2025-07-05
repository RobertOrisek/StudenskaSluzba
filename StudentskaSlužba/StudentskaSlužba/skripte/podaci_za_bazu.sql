use studentska_sluzba;

--insert into studenti(studentski_index, prezime, ime, grad)
--	values('E1 01/2011', 'Srđanov', 'Konstantin','Loznica'),
--			('E1 02/2012', 'Baki', 'Strahinja', 'Novi Sad'),
--			('E1 03/2013', 'Trajković', 'Nebojša', 'Inđija'),
--			('E2 01/2011', 'Sekulić', 'Miloš', 'Vršac'),
--			('E2 02/2012', 'Askin', 'Vuk', 'Novi Sad'),
--			('E2 03/2013', 'Klainić', 'Marko', 'Sombor'),
--			('E2 04/2011', 'Marko','Panić','Zrenjanin');

select * from studenti;

--insert into predmeti(naziv)
--	values ('Matematika'),
--			('Fizika'),
--			('Elektrotehnika'),
--			('Informatika');

select * from predmeti;

--insert into nastavnici (ime, prezime, zvanje)
--	values('Petar', 'Petrovic' ,'Docent'),
--			('Jovan', 'Jovanovic' ,'Docent'),
--			('Marko', 'Markovic' ,'Asistent'),
--			('Nikola', 'Nikolic' ,'Redovni Profesor'),
--			('Lazar', 'Lazic' ,'Asistent');

select * from nastavnici;

--insert into ispitni_rokovi(naziv, pocetak, kraj)
--	values('Januarski', '2015-01-15' ,'2015-01-29'),
--			('Februarski', '2015-02-01', '2015-02-14');

select * from ispitni_rokovi;

--insert into ispitne_prijave(student_id, predmet_id, ispitni_rok_id, teorija, zadaci)
--	values ('1', '1', '1', '88','89'),
--		   ('1', '2', '2', '85','55'),
--		   ('2', '2', '2', '80','45'),
--		   ('3', '1', '1', '94', '96'),
--		   ('4', '1', '1', '40', '60'),
--		   ('4', '1', '2', '83', '88'),
--		   ('4', '2', '2', '89', '91'),
--		   ('4', '4', '2', '100', '98'),
--		   ('5', '2', '1', '45', '47'),
--		   ('5', '3', '2', '56', '55'),
--		   ('6', '3', '2', '25', '0');

select * from ispitne_prijave;

--insert into predaje(nastavnik_id, predmet_id)
--	values	(1,1),
--			(1,2),
--			(1,3),
--			(2,2),
--			(3,2),
--			(4,1),
--			(4,3),
--			(4,4),
--			(5,3),
--			(5,4);

select * from predaje;

--insert into pohadja(student_id, predmet_id)
--	values (1,1),
--			(1,2),
--			(1,3),
--			(2,2),
--			(3,1),
--			(4,1),
--			(4,3),
--			(4,4),
--			(5,1),
--			(5,2),
--			(6,3);

select * from pohadja;