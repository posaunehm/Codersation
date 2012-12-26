package net.codersation.vendingmachine.money;

public class MoneyFlow {

    public MoneyPolicy moneyPolicy = new MoneyPolicy();

    private int saleAmount = 0;
    private final MoneyStock credit = new MoneyStock();
    private final MoneyStock change = new MoneyStock();
    private final MoneyStock pool = new MoneyStock();

    MoneyFlow(MoneyStock initialStock) {
        initialStock.moveAllMoneyTo(this.pool);
    }

    public int getSaleAmount() {
        return saleAmount;
    }

    private void addSale(int price) {
        saleAmount += price;
    }

    public int getCreditAmount() {
        return credit.getAmount();
    }

    public void insert(Money money) {
        (moneyPolicy.isAllowed(money) ? credit : change).add(money, 1);
    }

    public void payBack() {
        credit.moveAllMoneyTo(change);
    }

    public void purchase(int price) {
        // creditから払えるだけのお金を取り出す
        MoneyStock sale = credit.takeOut(price);
        // poolからお釣りのために差額のお金を取り出す
        int changeAmount = sale.getAmount() - price;
        if (!pool.canTakeOut(changeAmount)) {
            throw new IllegalStateException("cannot take out just Money. pool=" + pool + ", amount=" + changeAmount);
        }
        MoneyStock change = pool.takeOut(changeAmount);

        sale.moveAllMoneyTo(pool);
        change.moveAllMoneyTo(credit);

        addSale(price);
    }

    /**
     * 指定金額を購入した時にお釣りが返せるか
     *
     * @param price 購入する商品の金額
     * @return お釣りが返せるならtrue
     */
    public boolean canPayBackChange(int price) {
        return pool.canTakeOut(credit.getAmount() - price);
    }

    public MoneyStock takeOutChange() {
        MoneyStock out = new MoneyStock();
        change.moveAllMoneyTo(out);
        return out;
    }
}
