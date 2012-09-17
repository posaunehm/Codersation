package com.codersation.vendmachine;

import static org.hamcrest.CoreMatchers.is;
import static org.junit.Assert.assertThat;

import java.util.List;

import org.junit.Before;
import org.junit.Test;

import com.codersation.vendmachine.juice.Coke;
import com.codersation.vendmachine.money.AmountableMoneyFactory;

public class JuiceStocksTest {
	
	private JuiceStocks juiceStocks;
	
	@Before
	public void beforeTest() {
		juiceStocks = JuiceStocksFactory.createNewStocks();
	}

	@Test
	public void コーラの値段は120円である() {
		assertThat(new Coke().getPrice(), is(120));
	}

	@Test
	public void 在庫の初期状態はコーラが5本である() {
		List<JuiceStock> stocks = juiceStocks.getAllStocks();
		assertThat(stocks.get(0).getJuice().equals(new Coke()), is(true));
		assertThat(stocks.get(0).getStockCount(), is(5));
	}
	
	@Test
	public void 在庫が1個以上で投入金額が120円であればコーラが購入可能になる() throws Exception {
		Amount amount = new Amount();
		amount.insert(AmountableMoneyFactory.createNewMoney(100));
		amount.insert(AmountableMoneyFactory.createNewMoney(10));
		amount.insert(AmountableMoneyFactory.createNewMoney(10));
		
		assertThat(juiceStocks.isPurchasable(amount, new Coke()), is(true));
	}
	
	@Test
	public void 在庫が1個以上で投入金額が100円であればコーラは購入不可能になる() throws Exception {
		Amount amount = new Amount();
		amount.insert(AmountableMoneyFactory.createNewMoney(100));
		amount.insert(AmountableMoneyFactory.createNewMoney(10));
		
		assertThat(juiceStocks.isPurchasable(amount, new Coke()), is(false));
	}
	
	// TODO 在庫が0個の場合は130円投入していてもコーラは購入不可能になる
	// TODO 購入可能な状態で購入操作を行うとコーラの在庫が減る
	// TODO 購入可能な状態で購入操作を行うと売上金額が増える <= 他のクラスのテストケース
	// TODO 購入可能な状態で購入操作を行うと投入金額が0になる <= 他のクラスのテストケース
	// TODO 購入可能な状態で購入操作を行うとコーラと投入金額との差額が取得できる <= 他のクラスのテストケース
	// TODO 購入不可能な状態で購入操作を行ってもジュースの在庫は減らない
	// TODO 購入不可能な状態で購入操作を行っても売上金額は増えない <= 他のクラスのテストケース
	// TODO 購入不可能な状態で購入操作を行っても投入金額は減らない
	// TODO レッドブルを5本在庫に追加することができる
	// TODO 水を5本在庫に追加することができる
	// TODO 各商品の在庫が1個以上で投入金額が120円であれば購入可能商品一覧でコーラと水が取得できる
	// TODO 各商品の在庫が1個以上で投入金額が200円であれば購入可能商品一覧でコーラと水とレッドブルが取得できる
}
