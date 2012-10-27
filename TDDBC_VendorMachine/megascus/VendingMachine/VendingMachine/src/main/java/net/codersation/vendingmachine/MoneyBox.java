/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package net.codersation.vendingmachine;

import java.util.ArrayList;
import java.util.List;

/**
 * お金を集金する場所です。
 * お釣りに関しては書品を買うまでは投入されたままのお金、商品を買ったあとは、最低限のお金の数でお釣りが表されます。
 * @author megascus
 */
public class MoneyBox {
    
    private int totalAmount;
    
    /* 
     * 投入されたお金。支払いが行われたあとはクリアする。
     * 支払い後は必要に応じて再計算。
     */
    private List<Money> insertedMoney = new ArrayList<>();
    
    void insert(Money money) {
        this.insertedMoney.add(money);
        totalAmount += money.getValue();
    }
    
    /**
     * 投入された金額の合計を返します。
     * @return
     */
    public int getAmount() {
        return totalAmount;
    }
    
    /**
     * 支払いできるかを返します。
     * 
     * @param price
     * @return 
     */
    public boolean canPay(int price) {
        return totalAmount >= price;
    }
    
    /**
     * 支払います。
     * 
     * @param price 
     * @throws IllegalArgmentException cannot pay.
     */
    public void pay(int price) throws IllegalArgumentException {
        if(!canPay(price)) {
            throw new IllegalArgumentException("cannot pay.");
        }
        insertedMoney = null;
        totalAmount -= price;
    }
    
    /**
     * お釣りのお金を取得します。
     * 必要に応じて {@link #clear}を呼んで下さい。
     * @return 
     */
    public List<Money> getChange() {
        if(insertedMoney == null) {
            return amountToMoneys(totalAmount);
        }
        return new ArrayList<>(insertedMoney);
    }
    
    private List<Money> amountToMoneys(int amount) {
        List<Money> moneys = new ArrayList<>();
        while(true) {
            if(amount >= Money.ONE_THOUSAND.getValue()) {
                moneys.add(Money.ONE_THOUSAND);
                amount -= Money.ONE_THOUSAND.getValue();
            } else if(amount >= Money.FIVE_HUNDREDS.getValue()) {
                moneys.add(Money.FIVE_HUNDREDS);
                amount -= Money.FIVE_HUNDREDS.getValue();
            } else if(amount >= Money.ONE_HUNDRED.getValue()) {
                moneys.add(Money.ONE_HUNDRED);
                amount -= Money.ONE_HUNDRED.getValue();
            } else if(amount >= Money.FIFTY.getValue()) {
                moneys.add(Money.FIFTY);
                amount -= Money.FIFTY.getValue();
            } else if(amount >= Money.TEN.getValue()) {
                moneys.add(Money.TEN);
                amount -= Money.TEN.getValue();
            } else if(amount >= Money.FIVE.getValue()) {
                moneys.add(Money.FIVE);
                amount -= Money.FIVE.getValue();
            } else if(amount >= Money.ONE.getValue()) {
                moneys.add(Money.ONE);
                amount -= Money.ONE.getValue();
            } else {
                break;
            }
        }
        return moneys;
    }
    
    /**
     * 投入された金額を取り消します。
     */
    public void clear() {
        totalAmount = 0;
        insertedMoney = new ArrayList<>();
    }
    
}
