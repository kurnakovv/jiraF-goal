create database jiraf_user;

create table user(
	id_user uuid primary key,
	m_date_of_registration timestamp not null,
	m_name varchar(50) not null
);

insert into user (id_user, m_date_of_registration, m_name)
values(gen_random_uuid(), NOW(), 'Test');