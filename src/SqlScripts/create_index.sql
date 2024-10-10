CREATE INDEX IF NOT EXISTS "IX_participants_status_lifeboat_id"
    ON public.participants_status USING btree
    (lifeboat_id ASC NULLS LAST)
    TABLESPACE pg_default;