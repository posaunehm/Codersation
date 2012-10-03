package codersation.vendingmachine;

import static org.hamcrest.CoreMatchers.*;
import static org.junit.Assert.*;

import org.junit.Before;
import org.junit.Test;
import org.junit.experimental.runners.Enclosed;
import org.junit.runner.RunWith;

@RunWith(Enclosed.class)
public class VendingMachineTest {

	public static class 初期状態 {

		private VendingMachine sut;

		@Before
		public void setUp() {
			sut = new VendingMachine();
		}

		@Test
		public void 総計は0円() {
			assertThat(sut.getTotalAmount(), is(0));
		}

		@Test
		public void 十円投入すると総計は10円() throws Exception {
			sut.insert(Money.TenYen);
			assertThat(sut.getTotalAmount(), is(10));
		}

		@Test
		public void お釣りの金額は0円() throws Exception {
			assertThat(sut.getChangeAmount(), is(0));
		}

		@Test
		public void 二千円入れても総計は増えない() throws Exception {
			sut.insert(Money.TwoThousandYen);
			assertThat(sut.getTotalAmount(), is(0));
		}

		@Test
		public void 売上金額は0円() throws Exception {
			assertThat(sut.getSaleAmount(), is(0));
		}

		@Test
		public void 買えない状態で購入しても売上金額は増えない() throws Exception {
			sut.purchase(Juice.Coke);
			assertThat(sut.getSaleAmount(), is(0));
		}

		@Test
		public void 買えない状態で購入しても在庫は減らない() throws Exception {
			sut.purchase(Juice.Coke);
			assertThat(sut.getAllJuiceStock().get(0).getCount(), is(5));
		}

		@Test
		public void 在庫情報を取得できる() throws Exception {
			assertThat(sut.getAllJuiceStock().size(), is(3));
			assertThat(sut.getAllJuiceStock().get(0).getJuice(), is(Juice.Coke));
			assertThat(sut.getAllJuiceStock().get(1).getJuice(), is(Juice.Water));
			assertThat(sut.getAllJuiceStock().get(2).getJuice(), is(Juice.RedBull));
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
			assertThat(sut.getTotalAmount(), is(100));
		}

		@Test
		public void 払い戻すと総計が0円になる() throws Exception {
			sut.payBack();
			assertThat(sut.getTotalAmount(), is(0));
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
			sut.purchase(Juice.Coke);
			assertThat(sut.getTotalAmount(), is(100));
		}

		@Test
		public void コーラは買えない() throws Exception {
			assertThat(sut.getPurchasable(), not(hasItem(Juice.Coke)));
		}

		@Test
		public void 水は買える() throws Exception {
			assertThat(sut.getPurchasable(), hasItem(Juice.Water));
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
			assertThat(sut.getTotalAmount(), is(1000));
		}

		@Test
		public void 購入したら在庫が減る() throws Exception {
			sut.purchase(Juice.Coke);
			assertThat(sut.getAllJuiceStock().get(0).getCount(), is(4));
		}

		@Test
		public void 購入したら売上が増える() throws Exception {
			sut.purchase(Juice.Coke);
			assertThat(sut.getSaleAmount(), is(120));
		}

		@Test
		public void 購入したら預かり金が減る() throws Exception {
			sut.purchase(Juice.Coke);
			assertThat(sut.getTotalAmount(), is(880));
		}

		@Test
		public void 在庫が無くなったら購入不可になる() throws Exception {
			sut.purchase(Juice.Coke);
			sut.purchase(Juice.Coke);
			sut.purchase(Juice.Coke);
			sut.purchase(Juice.Coke);
			sut.purchase(Juice.Coke);

			assertThat(sut.getPurchasable(), not(hasItem(Juice.Coke)));
		}

		@Test
		public void コーラが買える() throws Exception {
			assertThat(sut.getPurchasable(), hasItem(Juice.Coke));
		}

		@Test
		public void レッドブルが買える() throws Exception {
			assertThat(sut.getPurchasable(), hasItem(Juice.RedBull));
		}
	}
}
