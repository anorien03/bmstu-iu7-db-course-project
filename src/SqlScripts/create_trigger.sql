CREATE OR REPLACE FUNCTION change_lifeboat_survived_count()
RETURNS TRIGGER
LANGUAGE plpgsql
AS $$
BEGIN
 	IF (TG_OP = 'INSERT') THEN
    	UPDATE lifeboats
    	SET survived_count = survived_count + 1
    	WHERE id = NEW.lifeboat_id;
		RETURN NEW;
	ELSEIF (TG_OP = 'DELETE') THEN
    	UPDATE lifeboats
    	SET survived_count = survived_count - 1
    	WHERE id = OLD.lifeboat_id;
		RETURN OLD;
	END IF;
    RETURN NULL;
END;
$$;

CREATE TRIGGER lifeboat_survived_count
AFTER INSERT OR DELETE ON participants_status
    FOR EACH ROW EXECUTE PROCEDURE change_lifeboat_survived_count();



CREATE OR REPLACE FUNCTION set_default_survived_count()
RETURNS TRIGGER
LANGUAGE plpgsql
AS $$
BEGIN
 	NEW.survived_count = 0;
    RETURN NEW;
END;
$$;

CREATE TRIGGER default_survived_count
BEFORE INSERT ON lifeboats
    FOR EACH ROW EXECUTE PROCEDURE set_default_survived_count();


-- drop TRIGGER lifeboat_survived_count ON participants_status;