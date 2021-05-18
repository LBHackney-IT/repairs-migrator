-- FUNCTION: migration.property_hierarchy(character varying)

-- DROP FUNCTION migration.property_hierarchy(character varying);

CREATE OR REPLACE FUNCTION migration.property_hierarchy(
	p_prop_ref character varying)
    RETURNS TABLE(root_prop_ref character varying, prop_ref character varying, short_address character varying, level_code character varying, prop_type text, depth integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
begin
	return query
		with recursive parents as (
			select dbo.property_uht_data_restore.prop_ref, dbo.property_uht_data_restore.short_address, dbo.property_uht_data_restore.major_ref, dbo.property_uht_data_restore.level_code, 0 as depth
			from dbo.property_uht_data_restore 
			where dbo.property_uht_data_restore.prop_ref = p_prop_ref
			union all
			select  p.prop_ref, p.short_address, p.major_ref, p.level_code, pr.depth - 1
			from dbo.property_uht_data_restore p,
				 parents pr
			where p.prop_ref = pr.major_ref
		)
		select p_prop_ref,
		parents.prop_ref,
			   parents.short_address,
			   parents.level_code,
			   CASE 
			  WHEN parents.level_code = '7'  THEN 'Dwelling'
			  WHEN parents.level_code = '6'  THEN 'Facility'
			  WHEN parents.level_code = '4'  THEN 'Sub block'
			  WHEN parents.level_code = '3'  THEN 'Block'
			  WHEN parents.level_code = '2'  THEN 'Estate'
			  WHEN parents.level_code = '0'  THEN 'Owner'
		END as type,
			   parents.depth
		from parents;
end;
$BODY$;

ALTER FUNCTION migration.property_hierarchy(character varying)
    OWNER TO repairs_admin;

GRANT EXECUTE ON FUNCTION migration.property_hierarchy(character varying) TO PUBLIC;

GRANT EXECUTE ON FUNCTION migration.property_hierarchy(character varying) TO repairs_admin;

GRANT EXECUTE ON FUNCTION migration.property_hierarchy(character varying) TO repairs_migration_user;

