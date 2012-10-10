module Spec.VendingMachineSpecification

open NaturalSpec

open VendingMachine



let initially vm =
    printMethod ()
    vm

let total_amount amount (vm:VendingMachine)  =
    printMethod amount 
    vm.TotalAmount = amount 

let insert_money amount (vm:VendingMachine) = 
    printMethod amount
    amount |> List.iter vm.InsertMoney
    vm
 
//Feature:お金が投入できる
[<Scenario>]
let ``After inserting 10 yen, it's total amount is 10``() =
  Given (new VendingMachine(new StandardMoneyAcceptor()))          
    |> When insert_money [new Money(MoneyKind.Yen10)]      
    |> It should have (total_amount 10)
    |> Verify

[<Scenario>]
let ``After inserting 10 yen and 100 yen, it's total amount is 110``() =
  Given (new VendingMachine(new StandardMoneyAcceptor()))          
    |> When insert_money [new Money(MoneyKind.Yen10);new Money(MoneyKind.Yen100)]  
    |> It should have (total_amount 110)
    |> Verify
                 
   
//Feature:投入金額の確認ができる
[<Scenario>]
let ``Initially, the total amount of this vending machine is 0``() =
  Given (new VendingMachine(new StandardMoneyAcceptor()))                
    |> When initially      
    |> It should have (total_amount 0)
    |> Verify


//Feature:扱えないお金を管理できる
[<Scenario>]
let ``After inserting 1 yen, it's invalid so machine's total amout is 0``() =
  Given (new VendingMachine(new StandardMoneyAcceptor()))                
    |> When insert_money [new Money(MoneyKind.Yen1)]      
    |> It should have (total_amount 0)
    |> Verify

//Feature：ジュースを購入する
let cola = new Drink("Cola", 110);
let soda = new Drink("Soda",150)

let inserted (moneyArr:List<Money>) (vm:VendingMachine) = 
    printMethod moneyArr
    moneyArr |> List.iter (fun x -> vm.InsertMoney(x))
    vm


let pushed (drinkArr:List<Drink>) (vm:VendingMachine) = 
    printMethod drinkArr
    drinkArr |> List.iter (fun x -> vm.AddDrink(x))
    vm
    
let buy_drink_named (drinkName:string) (vm:VendingMachine) =
    printMethod drinkName
    vm.BuyDrink(drinkName)

let drink_named name (drink:Drink) =
    printMethod name
    drink.Name = name


[<Scenario>]
let ``After inserting 110 yen, you can buy a juice less than 110 yen``() =
       Given (new VendingMachine(new StandardMoneyAcceptor())) 
            |> inserted [new Money(MoneyKind.Yen100);new Money(MoneyKind.Yen10)]
            |> pushed [cola]
        |> When buy_drink_named "Cola"
        |> It should have (drink_named "Cola")
        |> Verify

//お金が足りずにジュースを購入できない場合
[<Scenario>]
let ``After inserting 100 yen, you can't buy a juice over than 100 yen``() =
    Given (new VendingMachine(new StandardMoneyAcceptor())) 
            |> inserted [new Money(MoneyKind.Yen100);]
            |> pushed [cola]
        |> When buy_drink_named "Cola"
        |> It should equal null
        |> Verify

//在庫が足りずにジュースを購入できない場合
[<Scenario>]
let ``If there is no stock for supecified drink, you can't buy it``() =
    Given (new VendingMachine(new StandardMoneyAcceptor())) 
            |> inserted [new Money(MoneyKind.Yen100);]
            |> pushed [cola]
        |> When buy_drink_named "Soda"
        |> It should equal null
        |> Verify


//Feature：購入後の払い戻し
let bought (drinkArr:List<string>) (vm:VendingMachine) = 
    drinkArr |> List.iter (fun x -> ignore (vm.BuyDrink(x)))
    vm


let coin_50_yen_for length (moneySeq:seq<Money>) = 
    printMethod length
    let count = moneySeq |> Seq.filter (fun x -> x.Kind = MoneyKind.Yen50) |> Seq.length
    count = length

let coin_10_yen_for length (moneySeq:seq<Money>) = 
    printMethod length
    let count = moneySeq |> Seq.filter (fun x -> x.Kind = MoneyKind.Yen10) |> Seq.length
    count = length

let pay_back (vm:VendingMachine)  = 
    printMethod ()
    vm.PayBack().ToArray() |> Seq.ofArray

[<Scenario>]
let ``After inserted 200yen and then bought drink costed 110yen, you can pay back 90 yen``() =
    Given (new VendingMachine(new StandardMoneyAcceptor())) 
            |> inserted [new Money(MoneyKind.Yen100);new Money(MoneyKind.Yen100)]
            |> pushed [cola]
            |> bought ["Cola"]
        |> When pay_back
        |> It should have (coin_50_yen_for 1)
        |> It should have (coin_10_yen_for 4)
        |> Verify


[<Scenario>]
let ``After inserted 200yen and then bought drink costed 150yen, you can pay back 50 yen``() =
    Given (new VendingMachine(new StandardMoneyAcceptor())) 
            |> inserted [new Money(MoneyKind.Yen100);new Money(MoneyKind.Yen100)]
            |> pushed [soda]
            |> bought ["Soda"]
        |> When pay_back
        |> It should have (length 1)
        |> It should have (coin_50_yen_for 1)
        |> Verify

