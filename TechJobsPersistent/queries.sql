--Part 1
--##EMPLOYER
--Id - type int A1 PK
--Name - type LONGTEXT
--Location - type LONGTEXT

--##JOBS
-- ID - type int AI PK
-- NAME - LONGTEXT
--EmployerID - INT

--##JOBSKILLS
--JobId - int PK
--SkillId -INT PK

--##SKILLS
--Id - int AI PK
--Name - longtext
--Description - longtext

--Part 2
--Employers in St.Louis
--Centene
--Lockerdome
--Edward Jones
--Wells Fargo

--Part 3

--select jobs.Id, skills.name,skills.Description from jobs 
--left join jobskills on jobskills.JobId = jobs.Id
--left join skills on skills.Id = jobskills.SkillId
--where skills.name is not null
--order by name,description asc;