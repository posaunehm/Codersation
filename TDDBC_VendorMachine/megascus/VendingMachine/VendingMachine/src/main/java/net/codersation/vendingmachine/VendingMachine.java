/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package net.codersation.vendingmachine;

import java.util.List;

/**
 * 自動販売機です。
 * 10円玉、50円玉、100円玉、500円玉、1000円札10円玉、50円玉、100円玉、500円玉、1000円札が使用できます。
 *
 * @author megascus
 */
public class VendingMachine {

    private MoneyBox moneyBox = new MoneyBox();

    /**
     * お金を投入します。
     *
     * @return 使用出来る金種の場合はnull、使用できない金種の場合は投入した金種
     * @param money お金
     */
    public Money insert(Money money) {
        if (moneyBox.canInsert(money)) {
            moneyBox.insert(money);
            return null;
        }
        return money;
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
