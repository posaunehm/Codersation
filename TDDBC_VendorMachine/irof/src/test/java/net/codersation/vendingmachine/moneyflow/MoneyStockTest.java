package net.codersation.vendingmachine.moneyflow;

import static org.hamcrest.CoreMatchers.is;
import static org.junit.Assert.assertThat;
import net.codersation.vendingmachine.Money;

import org.junit.Test;

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

	@Test(expected = IllegalArgumentException.class)
	public void 払えない金額を要求されると例外を投げる() throws Exception {
		sut.takeOut(10);
	}
}
