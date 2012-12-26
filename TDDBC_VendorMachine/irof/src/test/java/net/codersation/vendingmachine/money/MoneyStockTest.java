package net.codersation.vendingmachine.money;

import org.junit.Test;

import static org.hamcrest.CoreMatchers.is;
import static org.junit.Assert.assertThat;
import static net.codersation.vendingmachine.money.Money.*;

public class MoneyStockTest {

    MoneyStock sut = new MoneyStock();

    @Test
    public void 百円三枚から120円を取り出すと200円() {
        sut.add(HundredYen, 3);

        MoneyStock actual = sut.takeOut(120);
        assertThat(actual.getAmount(), is(200));
    }

    @Test
    public void 百円二枚と五十円一枚から120円を取り出すと150円() {
        sut.add(HundredYen, 2);
        sut.add(FiftyYen, 1);

        MoneyStock actual = sut.takeOut(120);
        assertThat(actual.getAmount(), is(150));
    }

    @Test
    public void 百円二枚と十円三枚から120円を取り出すと120円() {
        sut.add(HundredYen, 2);
        sut.add(TenYen, 3);

        MoneyStock actual = sut.takeOut(120);
        assertThat(actual.getAmount(), is(120));
    }

    @Test
    public void canTakeOutは丁度払える場合にtrueを返す() throws Exception {
        sut.add(HundredYen, 2);
        sut.add(TenYen, 3);

        boolean actual = sut.canTakeOutJust(120);
        assertThat(actual, is(true));
    }

    @Test
    public void canTakeOutは丁度払えない金額を要求されるとfalseを返す() throws Exception {
        sut.add(HundredYen, 2);
        sut.add(TenYen, 3);

        boolean actual = sut.canTakeOutJust(140);
        assertThat(actual, is(false));
    }

    @Test(expected = IllegalArgumentException.class)
    public void 払えない金額を要求されると例外を投げる() throws Exception {
        sut.takeOut(10);
    }

    @Test
    public void 出力書式に応じて文字列表現を返す() {
        sut.add(HundredYen, 1);
        sut.add(TenYen, 1);

        String actual = sut.getText();
        assertThat(actual, is("10(1), 100(1)"));
    }
    @Test
    public void 出力書式に応じて文字列表現を返す_三角測量() {
        sut.add(FiftyYen, 2);

        String actual = sut.getText();
        assertThat(actual, is("50(2)"));
    }

    @Test
    public void 枚数指定で追加する() {
        sut.add(HundredYen, 5);

        assertThat(sut.getAmount(), is(500));
    }
}
