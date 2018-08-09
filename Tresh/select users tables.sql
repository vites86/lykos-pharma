/****** Script for SelectTopNRows command from SSMS  ******/

  SELECT clnt.name, usrs.email, clnt.rank, urls.Name, aspr.roleid
  FROM [Olga2].[dbo].[ClientProfiles] as clnt
  left join [Olga2].[dbo].[AspNetUserRoles] as aspr
  on clnt.id = aspr.UserId 
  left join [Olga2].[dbo].[AspNetRoles] as urls
   on urls.id = aspr.roleid
   left join [Olga2].[dbo].[AspNetUsers] as usrs
   on clnt.id = usrs.id
  where clnt.id like '936ff493-725b-43dd-863d-d6a533b7007a'  


SELECT *  FROM  [Olga2].[dbo].[AspNetRoles]
SELECT *  FROM  [Olga2].[dbo].[AspNetUsers]
SELECT *  FROM  [Olga2].[dbo].[ClientProfiles]

update [Olga2].[dbo].[AspNetRoles] set name = 'User' where name like 'Watcher'

    delete from [Olga2].[dbo].[AspNetUsers] where email like 'test@email.ua'
    delete from [Olga2].[dbo].[ClientProfiles] where Name like 'Tester'

	update [Olga2].[dbo].[AspNetUserRoles] set roleId = '20568b9b-c05f-4bb6-a0b7-6928ed814444'where UserId like '936ff493-725b-43dd-863d-d6a533b7007a'
    delete from [Olga2].[dbo].[AspNetUserRoles] where UserId like '936ff493-725b-43dd-863d-d6a533b7007a' and roleid like '20568b9b-c05f-4bb6-a0b7-6928ed813059'
