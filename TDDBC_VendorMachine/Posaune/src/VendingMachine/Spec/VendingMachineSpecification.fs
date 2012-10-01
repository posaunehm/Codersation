module Spec.VendingMachineSpecification

open NaturalSpec

open VendingMachine

let sut = new VendingMachine();

let initially vm =
    vm

let total_amount amount (vm:VendingMachine)  =
    printMethod (amount)
    vm.TotalAmount = amount 
    
//Feature:投入金額の確認ができる
[<Scenario>]
let ``Initially, the total amount of this vending machine is 0``() =
  Given sut              
    |> When initially      
    |> It should have (total_amount 0)
    |> Verify

//Feature:投入金額の確認ができる
                 