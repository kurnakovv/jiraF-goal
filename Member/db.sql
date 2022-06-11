create database jiraf_member;

create table member(
	id_member uuid primary key,
	m_date_of_registration timestamp not null,
	m_name varchar(50) not null
);

insert into member (id_member, m_date_of_registration, m_name)
values(gen_random_uuid(), NOW(), 'Test');