CREATE TABLE IF NOT EXISTS participants_close_relatives (
	"ParticipantsId" integer NOT NULL
		REFERENCES participants (id) ON DELETE CASCADE,
	"CloseRelativesId" integer NOT NULL
		REFERENCES close_relatives (id) ON DELETE CASCADE,
	PRIMARY KEY ("ParticipantsId", "CloseRelativesId")
	);

drop table participants_close_relatives;

select * from participants_close_relatives;
insert into close_relatives values (2, 'aaa', 'bbb', 'Male', '03.01.2001');

insert into participants_close_relatives values (1, 1);
select * from participants_close_relatives;

select * from close_relatives;





delete from participants_close_relatives;