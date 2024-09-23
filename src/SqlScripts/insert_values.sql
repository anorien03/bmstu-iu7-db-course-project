insert into participants (name, surname, position, gender, age) 
	values ('Edward', 'Smith', 'Captain', 'Male', 62);
insert into participants (name, surname, position, gender, age) 
	values ('Henry', 'Wilde', 'Chief Officer', 'Male', 39);

insert into lifeboats values (2, 'Karpatia', 3, 48);

select * from participants;

select * from lifeboats;
select * from participants_status;

insert into participants_status values (3, null, 'Survived', 2);
delete from participants_status where participant_id = 3;
delete from lifeboats where id = 2;

