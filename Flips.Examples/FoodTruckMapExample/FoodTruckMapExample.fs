﻿module Flips.Examples.FoodTruckMapExample

open Flips
open Flips.Legacy


let solve settings =
    let model, objective = SampleProblems.FoodTruckMapExample.model
    // Call the `solve` function in the Solve module to evaluate the model
    let result = Solver.solve settings model

    printfn "-- Result --"

    // Match the result of the call to solve
    // If the model could not be solved it will return a `Suboptimal` case with a message as to why
    // If the model could be solved, it will print the value of the Objective Function and the
    // values for the Decision Variables
    match result with
    | Optimal solution ->
        printfn "Objective Value: %f" (Objective.evaluate solution objective)
                
        CsvExport.exportVariablesToFile "foodtruck.map" solution.DecisionResults CsvExport.csvConfig
                

        for (decision, value) in solution.DecisionResults |> Map.toSeq do
            let (DecisionName name) = decision.Name
            printfn "Decision: %s\tValue: %f" name value
    | errorCase -> 
        printfn "Unable to solve. Error: %A" errorCase
