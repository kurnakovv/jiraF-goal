create database jiraf_goal;

create table goal(
	id_goal uuid primary key,
	g_title varchar(50) not null,
	g_description varchar(500) not null,
	g_reporter_id uuid not null,
	g_assignee_id uuid,
	g_date_of_create timestamp not null,
	g_date_of_update timestamp,
	g_label_id uuid
);

create table label (
	id_label uuid primary key,
	l_title varchar(50) not null
);

insert into goal (id_goal, g_title, g_description, g_reporter_id, g_assignee_id, g_date_of_create, g_date_of_update, g_label_id)
values(gen_random_uuid(), 'Test', 'Test', gen_random_uuid(), gen_random_uuid(), NOW(), NOW(), gen_random_uuid());

insert into label (id_label, l_title)
values (gen_random_uuid(), 'Test');