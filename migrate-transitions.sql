UPDATE ActivacionProfetica.dbo.XpoStateMachine SET StartState = null

INSERT INTO RAPJUVE2024.dbo.XpoStateMachine
SELECT *
FROM ActivacionProfetica.dbo.XpoStateMachine


INSERT INTO RAPJUVE2024.dbo.XpoState
SELECT *
FROM ActivacionProfetica.dbo.XpoState

UPDATE RAPJUVE2024.dbo.XpoStateMachine SET StartState = 'CB2482CE-E855-4B56-8D58-36B5C95E039E' 

INSERT INTO RAPJUVE2024.dbo.XpoTransition
SELECT *
FROM ActivacionProfetica.dbo.XpoTransition




UPDATE RAPJUVE2024.dbo.XpoState SET TargetObjectCriteria = '[PrimerCuotaPagada] = True' WHERE Oid = '9EC5BF87-CD6C-4077-8240-7005A5BB63F4'


