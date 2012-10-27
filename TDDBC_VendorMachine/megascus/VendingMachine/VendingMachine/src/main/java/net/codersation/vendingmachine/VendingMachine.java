/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package net.codersation.vendingmachine;

import java.util.List;

/**
 * 自動販売機です。
 * @author megascus
 */
public class VendingMachine {

    private MoneyBox moneyBox = new MoneyBox();

    /**
     * お金を投入します。
     * 
     * @param money お金
     */
    public void insert(Money money) {
        moneyBox.insert(money);
    }

    /**
     * 投入金額を取得します。
     * 
     * @return 投入金額 
     */
    public int getTotalAmount() {
        return moneyBox.getAmount();
    }

    /**
     * お釣りを取得します。
     * 
     * @return お釣り
     */
    public List<Money> pickChange() {
        List<Money> moneys = moneyBox.getChange();
        moneys.clear();
        return moneys;
    }
}
