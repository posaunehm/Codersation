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
     * @param send 中身を渡されるMoneyStock
     */
    void moveAllMoneyTo(MoneyStock send) {
        for (Map.Entry<Money, Integer> entry : stock.entrySet()) {
            send.add(entry.getKey(), entry.getValue());
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

        MoneyStock res = createTakeOutStock(price);
        // 取り出す分を減らす
        for (Map.Entry<Money, Integer> entry : res.stock.entrySet()) {
            add(entry.getKey(), entry.getValue() * -1);
        }
        return res;
    }

    /**
     * 指定金額取り出します
     * @param amount 金額
     * @return 取りだされるもの
     */
    private MoneyStock createTakeOutStock(int amount) {
        MoneyStock res = getMoneyStock(amount, true);

        // 越えてる分を削る
        int diff = amount - res.getAmount();
        if (diff < 0) {
            MoneyStock res2 = res.getMoneyStock(diff * -1, false);
            // 取り出す分を減らす
            for (Map.Entry<Money, Integer> entry : res2.stock.entrySet()) {
                res.add(entry.getKey(), entry.getValue() * -1);
            }
        }

        return res;
    }

    private MoneyStock getMoneyStock(int amount, boolean flg) {
        MoneyStock res = new MoneyStock();
        for (Map.Entry<Money, Integer> entry : stock.entrySet()) {
            Money money = entry.getKey();
            int count = entry.getValue();
            int use = (amount - res.getAmount()) / money.getValue() + (flg ? 1 : 0);
            if (use > count) {
                use = count;
            }

            res.add(money, use);
            if (res.getAmount() >= amount) {
                break;
            }
        }
        return res;
    }

    /**
     * 指定金額丁度取り出せるかを返します
     * @param amount 金額
     * @return 丁度返せるならtrue
     */
    public boolean canTakeOutJust(int amount) {
        return amount == createTakeOutStock(amount).getAmount();
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
