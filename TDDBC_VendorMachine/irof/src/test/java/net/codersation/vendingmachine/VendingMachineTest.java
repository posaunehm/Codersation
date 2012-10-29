package net.codersation.vendingmachine;

import static org.hamcrest.CoreMatchers.*;
import static org.junit.Assert.*;

import net.codersation.vendingmachine.Juice;
import net.codersation.vendingmachine.Money;
import net.codersation.vendingmachine.VendingMachine;

import org.junit.Before;
import org.junit.Test;
import org.junit.experimental.runners.Enclosed;
import org.junit.runner.RunWith;

@RunWith(Enclosed.class)
public class VendingMachineTest {

	private static final Juice コーラ = JuiceFactory.create("コーラ");
	private static final Juice レッドブル = JuiceFactory.create("レッドブル");
	private static final Juice 水 = JuiceFactory.create("水");

	public static class 初期状態 {

		private VendingMachine sut;

		@Before
		public void setUp() {
			sut = new VendingMachine();
		}

		@Test
		public void 総計は0円() {
			assertThat(sut.getCreditAmount(), is(0));
		}

		@Test
		public void 十円投入すると総計は10円() throws Exception {
			sut.insert(Money.TenYen);
			assertThat(sut.getCreditAmount(), is(10));
		}

		@Test
		public void お釣りの金額は0円() throws Exception {
			assertThat(sut.getChangeAmount(), is(0));
		}

		@Test
		public void 二千円入れても総計は増えない() throws Exception {
			sut.insert(Money.TwoThousandYen);
			assertThat(sut.getCreditAmount(), is(0));
		}

		@Test
		public void 売上金額は0円() throws Exception {
			assertThat(sut.getSaleAmount(), is(0));
		}

		@Test
		public void 買えない状態で購入しても売上金額は増えない() throws Exception {
			sut.purchase(コーラ);
			assertThat(sut.getSaleAmount(), is(0));
		}

		@Test
		public void 買えない状態で購入しても在庫は減らない() throws Exception {
			sut.purchase(コーラ);
			assertThat(sut.getStockCount(コーラ), is(5));
		}

		@Test
		public void 在庫情報を取得できる() throws Exception {
			StockReport actual = sut.getAllJuiceStock();
			assertThat(actual.size(), is(3));
			assertThat(actual.get(コーラ), is(5));
			assertThat(actual.get(JuiceFactory.create("レッドブル")), is(5));
			assertThat(actual.get(JuiceFactory.create("水")), is(5));
		}
	}

	public static class 百円入れた状態 {

		private VendingMachine sut;

		@Before
		public void setUp() {
			sut = new VendingMachine();
			sut.insert(Money.HundredYen);
		}

		@Test
		public void 総計は100円() throws Exception {
			assertThat(sut.getCreditAmount(), is(100));
		}

		@Test
		public void 払い戻すと総計が0円になる() throws Exception {
			sut.payBack();
			assertThat(sut.getCreditAmount(), is(0));
		}

		@Test
		public void 払い戻すとお釣りが増える() throws Exception {
			sut.payBack();
			assertThat(sut.getChangeAmount(), is(100));
		}

		@Test
		public void 払い戻しを二回してもお釣りは変わらない() throws Exception {
			sut.payBack();
			sut.payBack();
			assertThat(sut.getChangeAmount(), is(100));
		}

		@Test
		public void 買えない状態で購入しても預かり金は減らない() throws Exception {
			sut.purchase(コーラ);
			assertThat(sut.getCreditAmount(), is(100));
		}

		@Test
		public void コーラは買えない() throws Exception {
			assertThat(sut.getPurchasable(), not(hasItem(コーラ)));
		}

		@Test
		public void 水は買える() throws Exception {
			assertThat(sut.getPurchasable(), hasItem(水));
		}
	}

	public static class 千円入れた状態 {

		private VendingMachine sut;

		@Before
		public void setUp() {
			sut = new VendingMachine();
			sut.insert(Money.FiveHundredYen);
			sut.insert(Money.FiveHundredYen);
		}

		@Test
		public void 総計は1000円() throws Exception {
			assertThat(sut.getCreditAmount(), is(1000));
		}

		@Test
		public void 購入したら在庫が減る() throws Exception {
			sut.purchase(コーラ);
			assertThat(sut.getStockCount(コーラ), is(4));
		}

		@Test
		public void 購入したら売上が増える() throws Exception {
			sut.purchase(コーラ);
			assertThat(sut.getSaleAmount(), is(120));
		}

		@Test
		public void 購入したら預かり金が減る() throws Exception {
			sut.purchase(コーラ);
			assertThat(sut.getCreditAmount(), is(880));
		}

		@Test
		public void 在庫が無くなったら購入不可になる() throws Exception {
			sut.purchase(コーラ);
			sut.purchase(コーラ);
			sut.purchase(コーラ);
			sut.purchase(コーラ);
			sut.purchase(コーラ);

			assertThat(sut.getPurchasable(), not(hasItem(コーラ)));
		}

		@Test
		public void コーラが買える() throws Exception {
			assertThat(sut.getPurchasable(), hasItem(コーラ));
		}

		@Test
		public void レッドブルが買える() throws Exception {
			assertThat(sut.getPurchasable(), hasItem(レッドブル));
		}
	}
}
