# ApproviaChallenge.TaskManager

Web Service to manage a Task list, and also give an analysis based on the task time allocation

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)


## Introduction

Web Service to manage a Task list, and also give an analysis based on the task time allocation
1. create a task, using an object construct with following property.
   string TaskID 
   string TaskName
   string TaskDescription
   DateTime StartDate
   int AllottedTimeInDays
   int ElapsedTimeInDays
   bool TaskStatus
2. Retrieve a task, from the data store
3. Delete a task from the data store

## Features
1. Microsoft.Extensions.Logging (sink to the console)
2. DataStore https://mockapi.io/ (document object database)
3. Design Architecture (Repository Pattern)
4. WebService (Rest API Servicess)

## Getting Started
a.) clone the application
b.) have a .net 6.0 runtime installed
c.) install visual studio or visual studio code
d.) open the ApproviaChallenge.TaskManager.sln and run 
OR 
e.) using command prompt run 
    dotnet build 
    dotnet run

