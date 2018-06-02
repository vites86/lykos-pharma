INSERT [info].[ProductNames] ([Name], [CountryId]) VALUES ('Artrosilene '2)
INSERT [info].[ProductNames] ([Name], [CountryId]) VALUES ('Artrosilene '2)
INSERT [info].[ProductNames] ([Name], [CountryId]) VALUES ('Fluifort '2)
INSERT [info].[ProductNames] ([Name], [CountryId]) VALUES ('OKI '2)
INSERT [info].[ProductNames] ([Name], [CountryId]) VALUES ('Dafnegin '2)
INSERT [info].[ProductNames] ([Name], [CountryId]) VALUES ('Voltaren Rapid® '2)
INSERT [info].[ProductNames] ([Name], [CountryId]) VALUES ('Erdomed  '2)
INSERT [info].[ProductNames] ([Name], [CountryId]) VALUES ('Ermucin  '2)
INSERT [info].[ProductNames] ([Name], [CountryId]) VALUES ('GLIATILIN '2)
INSERT [info].[ProductNames] ([Name], [CountryId]) VALUES ('MACMIROR® COMPLEX '2)
INSERT [info].[ProductNames] ([Name], [CountryId]) VALUES ('MACMIROR '2)
INSERT [info].[ProductNames] ([Name], [CountryId]) VALUES ('Onicit® '2)
INSERT [info].[ProductNames] ([Name], [CountryId]) VALUES ('Trittico '2)


 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like '') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ()


 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Aerosol, 15%') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Aerosol, 15%')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Capsules') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Capsules')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Solution for injection') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Solution for injection')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Gel') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Gel')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Granules for oral susp') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Granules for oral susp')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Syrup') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Syrup')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Granules for oral solution') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Granules for oral solution')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Solution for local use') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Solution for local use')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Rectal suppositories for children') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Rectal suppositories for children')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Rectal suppositories') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Rectal suppositories')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Vaginal cream') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Vaginal cream')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Vaginal suppositories') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Vaginal suppositories')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Powder for oral solution') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Powder for oral solution')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Capsules') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Capsules')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Granules for oral susp') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Granules for oral susp')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Capsules') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Capsules')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Solution for injection') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Solution for injection')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Vaginal cream ') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Vaginal cream')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Vaginal suppositories') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Vaginal suppositories')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Coated tablets') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Coated tablets')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Solution for intravenous administration') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Solution for intravenous administration')
 if not exists (select * from [Olga2].[info].[PharmaceuticalForms] where PharmaForm like 'Tablets') insert into [Olga2].[info].[PharmaceuticalForms](PharmaForm) values ('Tablets')


if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '25 ml') insert into [Olga2].[info].[Strengths](Strngth) values ('25 ml')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '320 mg') insert into [Olga2].[info].[Strengths](Strngth) values ('320 mg')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '80 mg/ml') insert into [Olga2].[info].[Strengths](Strngth) values ('80 mg/ml')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '2.7 g/5 g') insert into [Olga2].[info].[Strengths](Strngth) values ('2.7 g/5 g')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '90 mg/ml') insert into [Olga2].[info].[Strengths](Strngth) values ('90 mg/ml')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '80 ml') insert into [Olga2].[info].[Strengths](Strngth) values ('80 ml')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '16 mg/ml') insert into [Olga2].[info].[Strengths](Strngth) values ('16 mg/ml')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '60 mg') insert into [Olga2].[info].[Strengths](Strngth) values ('60 mg')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '160 mg') insert into [Olga2].[info].[Strengths](Strngth) values ('160 mg')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '78 g') insert into [Olga2].[info].[Strengths](Strngth) values ('78 g')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '100 mg') insert into [Olga2].[info].[Strengths](Strngth) values ('100 mg')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '50 mg') insert into [Olga2].[info].[Strengths](Strngth) values ('50 mg')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '300 mg') insert into [Olga2].[info].[Strengths](Strngth) values ('300 mg')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '175 mg/5 ml') insert into [Olga2].[info].[Strengths](Strngth) values ('175 mg/5 ml')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '400 mg') insert into [Olga2].[info].[Strengths](Strngth) values ('400 mg')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '1000 mg /4 ml') insert into [Olga2].[info].[Strengths](Strngth) values ('1000 mg /4 ml')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '30 g') insert into [Olga2].[info].[Strengths](Strngth) values ('30 g')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '500 mg ') insert into [Olga2].[info].[Strengths](Strngth) values ('500 mg')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '200 mg') insert into [Olga2].[info].[Strengths](Strngth) values ('200 mg')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '0.25 mg/5 ml') insert into [Olga2].[info].[Strengths](Strngth) values ('0.25 mg/5 ml')
if not exists (select * from [Olga2].[info].[Strengths] where [Strngth] like '150 mg') insert into [Olga2].[info].[Strengths](Strngth) values ('150 mg')


25 ml
320 mg
80 mg/ml
2.7 g/5 g
90 mg/ml
80 ml
16 mg/ml
60 mg
160 mg
78 g
100 mg
50 mg
300 mg
175 mg/5 ml
400 mg
1000 mg /4 ml
30 g
500 mg
200 mg
0.25 mg/5 ml
150 mg
