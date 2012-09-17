package com.codersation.vendmachine;

import static org.hamcrest.CoreMatchers.is;
import static org.junit.Assert.assertThat;

import org.junit.Before;
import org.junit.Test;

import com.codersation.vendmachine.money.AmountableMoneyFactory;

public class AmountTest {
	
	private Amount amount;
	
	@Before
	public void beforeTest() {
		amount = new Amount();
	}
	
	@Test
	public void 百円と十円を投入すると投入金額合計が百十円になる() throws Exception {
		amount.insert(AmountableMoneyFactory.createNewMoney(100));
		amount.insert(AmountableMoneyFactory.createNewMoney(10));
		assertThat(amount.getTotal(), is(110));
	}
	
	@Test
	public void 五百円と千円と五十円を投入する投入金額合計が千五百五十円になる() throws Exception {
		amount.insert(AmountableMoneyFactory.createNewMoney(500));
		amount.insert(AmountableMoneyFactory.createNewMoney(1000));
		amount.insert(AmountableMoneyFactory.createNewMoney(50));
		assertThat(amount.getTotal(), is(1550));
	}
	
	@Test
	public void 百円と十円を投入後に払い戻しを行うと百十円が取得できる() throws Exception {
		amount.insert(AmountableMoneyFactory.createNewMoney(100));
		amount.insert(AmountableMoneyFactory.createNewMoney(10));
		assertThat(amount.payBack(), is(110));
	}
	
	@Test
	public void 払い戻しを行うと投入金額合計が0円になる() throws Exception {
		amount.insert(AmountableMoneyFactory.createNewMoney(100));
		amount.insert(AmountableMoneyFactory.createNewMoney(50));
		amount.payBack();
		assertThat(amount.getTotal(), is(0));
	}
	
	@Test
	public void 未投入の状態で払い戻しを行うと0円を取得できる() {
		assertThat(amount.payBack(), is(0));
	}
	
	@Test(expected = Exception.class)
	public void 一万円では例外がスローされる() throws Exception {
		AmountableMoneyFactory.createNewMoney(10000);
	}
	
	@Test(expected = Exception.class)
	public void 一円では例外がスローされる() throws Exception {
		AmountableMoneyFactory.createNewMoney(1);
	}

}
