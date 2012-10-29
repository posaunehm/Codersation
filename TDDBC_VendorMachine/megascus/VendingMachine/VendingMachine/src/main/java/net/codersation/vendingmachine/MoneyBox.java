/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package net.codersation.vendingmachine;

import java.util.ArrayList;
import java.util.List;
import megascus.util.ListUtils;

/**
 * お金を集金する場所です。 お釣りに関しては商品を買うまでは投入されたままのお金、商品を買ったあとは、最低限のお金の数でお釣りが表されます。
 *
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
     *
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
        if (!canPay(price)) {
            throw new IllegalArgumentException("cannot pay.");
        }
        insertedMoney = null;
        totalAmount -= price;
    }

    /**
     * お釣りのお金を取得します。 必要に応じて {@link #clear}を呼んで下さい。
     *
     * @return
     */
    public List<Money> getChange() {
        if (insertedMoney == null) {
            return amountToMoneys(totalAmount);
        }
        return new ArrayList<>(insertedMoney);
    }
    private List<Money> canPayChargeMoneys = ListUtils.of(Money.ONE_THOUSAND,
            Money.FIVE_HUNDREDS, Money.ONE_HUNDRED, Money.FIFTY, Money.TEN,
            Money.FIVE, Money.ONE);

    private List<Money> amountToMoneys(int amount) {
        List<Money> moneys = new ArrayList<>();
        
        for (Money charge : canPayChargeMoneys) {
            while(amount >= charge.getValue()) {
                moneys.add(charge);
                amount -= charge.getValue();
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
