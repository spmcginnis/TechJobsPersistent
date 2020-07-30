--Part 1: For the jobs table, list the columns and their data types.
SELECT COLUMN_NAME, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'jobs';

--Part 2: write a query to list the names of the employers in St. Louis City.
SELECT * FROM EMPLOYERS
WHERE LOCATION = 'SAINT LOUIS';

--Part 3: write a query to return a list of the names and descriptions of all skills that are attached to jobs in alphabetical order. If a skill does not have a job listed, it should not be included in the results of this query.

SELECT NAME, DESCRIPTION
FROM SKILLS
INNER JOIN JOBSKILLS ON SKILLS.ID = JOBSKILLS.SKILLID
ORDER BY NAME;
