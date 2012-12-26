package net.codersation.vendingmachine.money;

import java.util.EnumMap;
import java.util.Map;

public class MoneyStock {

    private Map<Money, Integer> stock = new EnumMap<>(Money.class);

    public int getAmount() {
        int amount = 0;

        for (Map.Entry<Money, Integer> entry : stock.entrySet()) {
            amount += entry.getKey().getValue() * entry.getValue();
        }
        return amount;
    }

    /**
     * 引数のMoneyStockにこのオブジェクトに全てのStockを移す。移されたほうは空になる。
     *
     * @param s 中身を渡されるMoneyStock
     */
    void moveAllMoneyTo(MoneyStock s) {
        for (Map.Entry<Money, Integer> entry : stock.entrySet()) {
            s.add(entry.getKey(), entry.getValue());
        }
        stock.clear();
    }

    /**
     * 指定された金額のMoneyStockを取り出す。
     * 取り出された分の金額は減る。
     * TODO 丁度、多め、などは TakeOutRule とかにしたい
     *
     * @param price 取り出したい金額
     * @return 取り出されたMoneyStock
     */
    public MoneyStock takeOut(int price) {
        if (price > getAmount())
            throw new IllegalArgumentException("cannot take out. price:" + price + ", amount " + getAmount());

        MoneyStock res = calc(price);
        // 取り出す分を減らす
        for (Map.Entry<Money, Integer> entry : res.stock.entrySet()) {
            stock.put(entry.getKey(), stock.get(entry.getKey()) - entry.getValue());
        }
        return res;
    }

    private MoneyStock calc(int amount) {
        MoneyStock res = getMoneyStock(amount, true);

        // 越えてる分を削る
        int diff = amount - res.getAmount();
        if (diff < 0) {
            MoneyStock res2 = res.getMoneyStock(diff * -1, false);
            // 取り出す分を減らす
            for (Map.Entry<Money, Integer> entry : res2.stock.entrySet()) {
                res.stock.put(entry.getKey(), res.stock.get(entry.getKey()) - entry.getValue());
            }
        }

        return res;
    }

    private MoneyStock getMoneyStock(int amount, boolean flg) {
        MoneyStock res = new MoneyStock();
        for (Map.Entry<Money, Integer> entry : stock.entrySet()) {
            Money money = entry.getKey();
            int count = entry.getValue();
            int use = amount / money.getValue() + (flg ? 1 : 0);
            if (use > count) {
                use = count;
            }
            amount -= money.getValue() * use;

            res.add(money, use);
            if (amount < 0) {
                break;
            }
        }
        return res;
    }

    public boolean canTakeOut(int amount) {
        return amount == calc(amount).getAmount();
    }

    @Override
    public String toString() {
        return getText();
    }

    public void add(Money money, int count) {
        if (stock.containsKey(money)) {
            stock.put(money, stock.get(money) + count);
        } else {
            stock.put(money, count);
        }
    }

    public String getText() {
        StringBuilder sb = new StringBuilder();
        for (Map.Entry<Money, Integer> entry : stock.entrySet()) {
            if (sb.length() != 0) sb.append(", ");
            sb.append(String.format("%d(%d)", entry.getKey().getValue(), entry.getValue()));
        }
        return sb.toString();
    }
}
