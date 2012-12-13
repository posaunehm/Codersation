package net.codersation.vendingmachine.money;

import static org.hamcrest.CoreMatchers.is;
import static org.junit.Assert.*;

import org.junit.Test;

public class MoneyFlowTest {

	@Test
	public void 千円入れて百二十円購入() {
		MoneyFlow sut = MoneyFlowFactory.create();
		sut.insert(Money.ThousandYen);

		sut.purchase(120);

		assertThat(sut.getCreditAmount(), is(880));
		assertThat(sut.getSaleAmount(), is(120));
		assertThat(sut.takeOutChange().getAmount(), is(0));
	}

	@Test(expected=IllegalStateException.class)
	public void 千円入れて百二十円購入して戻してを二回() {
		MoneyFlow sut = MoneyFlowFactory.create();
		sut.insert(Money.ThousandYen);
		sut.purchase(120);
		sut.payBack();
		sut.insert(Money.ThousandYen);
		sut.purchase(120);
	}
}
