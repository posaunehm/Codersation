package net.codersation.vendingmachine.money;

import org.junit.Test;

import static org.hamcrest.CoreMatchers.is;
import static org.junit.Assert.assertThat;

public class MoneyStockTest {

    MoneyStock sut = new MoneyStock();

    private void add(Money... moneys) {
        for (Money money : moneys) {
            sut.add(money);
        }
    }

    @Test
    public void 百円三枚から120円を取り出すと200円() {
        add(Money.HundredYen, Money.HundredYen, Money.HundredYen);
        MoneyStock actual = sut.takeOut(120);
        assertThat(actual.getAmount(), is(200));
    }

    @Test
    public void 百円二枚と五十円一枚から120円を取り出すと150円() {
        add(Money.HundredYen, Money.HundredYen, Money.FiftyYen, Money.TenYen);
        MoneyStock actual = sut.takeOut(120);
        assertThat(actual.getAmount(), is(150));
    }

    @Test
    public void 百円二枚と十円三枚から120円を取り出すと120円() {
        add(Money.HundredYen, Money.HundredYen, Money.TenYen, Money.TenYen, Money.TenYen);
        MoneyStock actual = sut.takeOut(120);
        assertThat(actual.getAmount(), is(120));
    }

    @Test
    public void canTakeOutは丁度払える場合にtrueを返す() throws Exception {
        add(Money.HundredYen, Money.HundredYen, Money.TenYen, Money.TenYen, Money.TenYen);
        boolean actual = sut.canTakeOut(120);
        assertThat(actual, is(true));
    }

    @Test
    public void canTakeOutは丁度払えない金額を要求されるとfalseを返す() throws Exception {
        add(Money.HundredYen, Money.HundredYen, Money.TenYen, Money.TenYen, Money.TenYen);
        boolean actual = sut.canTakeOut(140);
        assertThat(actual, is(false));
    }

    @Test(expected = IllegalArgumentException.class)
    public void 払えない金額を要求されると例外を投げる() throws Exception {
        sut.takeOut(10);
    }

    @Test
    public void 出力書式に応じて文字列表現を返す() {
        sut.add(Money.HundredYen);
        sut.add(Money.TenYen);
        String actual = sut.toString(new ConsoleFormatter());
        assertThat(actual, is("10(1), 100(1)"));
    }
    @Test
    public void 出力書式に応じて文字列表現を返す_三角測量() {
        sut.add(Money.FiftyYen);
        sut.add(Money.FiftyYen);
        String actual = sut.toString(new ConsoleFormatter());
        assertThat(actual, is("50(2)"));
    }

    @Test
    public void 枚数指定で追加する() {
        sut.add(Money.HundredYen, 5);

        assertThat(sut.getAmount(), is(500));
    }
}
