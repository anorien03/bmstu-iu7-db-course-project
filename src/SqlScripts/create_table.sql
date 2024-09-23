CREATE TABLE IF NOT EXISTS participants (
	id integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY, 
	name varchar(64),
	surname varchar(64),
	age integer NOT NULL CHECK (age >= 0 AND age < 150),
	gender varchar(8) NOT NULL CHECK (gender IN ('Male', 'Female')),
	position varchar(128) NOT NULL
	);


CREATE TABLE IF NOT EXISTS users (
	id integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY, 
	login varchar(64) NOT NULL UNIQUE,
	password varchar(128) NOT NULL,
	role varchar(16) NOT NULL CHECK (role IN ('Admin', 'Researcher', 'User')),
	);


CREATE TABLE IF NOT EXISTS passengers (
	participant_id integer NOT NULL PRIMARY KEY
		REFERENCES participants (id) ON DELETE CASCADE, 
	departure varchar(64),
	destination varchar(64),
	class varchar(8) NOT NULL CHECK (class IN ('First', 'Second', 'Third')),
	);


CREATE TABLE IF NOT EXISTS bodies (
	id integer NOT NULL PRIMARY KEY,
	boat varchar(128) NOT NULL,
	date date NOT NULL CHECK (date >= '14-Apr-1912')
	);


CREATE TABLE IF NOT EXISTS lifeboats (
	id integer NOT NULL PRIMARY KEY,
	boat varchar(128) NOT NULL,
	survived_count integer NOT NULL CHECK (survived_count >= 0),
	max_count integer NOT NULL CHECK (max_count > 0)
	);


CREATE TABLE IF NOT EXISTS participants_status (
	participant_id integer NOT NULL PRIMARY KEY 
		REFERENCES participants (id) ON DELETE CASCADE, 
	status varchar(16) NOT NULL CHECK (status IN ('Survived', 'Victim')),
	lifeboat_id integer REFERENCES lifeboats (id) ON DELETE SET NULL,
	body_number integer references bodies (id) ON DELETE SET NULL
	);


CREATE TABLE IF NOT EXISTS close_relatives (
	id integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY, 
	name varchar(64) NOT NULL,
	surname varchar(64) NOT NULL,
	age integer NOT NULL CHECK (age >= 0 AND age < 150),
	gender varchar(8) NOT NULL CHECK (gender IN ('Male', 'Female')),
	);


CREATE TABLE IF NOT EXISTS participants_close_relatives (
	participant_id integer NOT NULL
		REFERENCES participants (id) ON DELETE CASCADE,
	relative_id integer NOT NULL
		REFERENCES participants (id) ON DELETE CASCADE,
	PRIMARY KEY (participant_id, relative_id)
	);


