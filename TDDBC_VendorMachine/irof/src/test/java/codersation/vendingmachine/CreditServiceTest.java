package codersation.vendingmachine;

import static org.hamcrest.CoreMatchers.is;
import static org.junit.Assert.assertThat;
import static org.junit.matchers.JUnitMatchers.everyItem;
import static org.junit.matchers.JUnitMatchers.hasItems;

import java.util.Arrays;
import java.util.List;

import org.junit.Test;

public class CreditServiceTest {

	@Test
	public void 百円三枚あるところで120円の買い物をすると百円二枚使う() {
		List<Money> list = Arrays.asList(Money.HundredYen, Money.HundredYen, Money.HundredYen);
		List<Money> actual = CreditService.getUseMoneyList(list, 120);
		assertThat(actual.size(), is(2));
		assertThat(actual, everyItem(is(Money.HundredYen)));
	}

	@Test
	public void 百円二枚と五十円一枚あるところで120円の買い物をすると百円と五十円を使う() {
		List<Money> list = Arrays.asList(Money.HundredYen, Money.HundredYen, Money.FiftyYen, Money.TenYen);
		List<Money> actual = CreditService.getUseMoneyList(list, 120);
		assertThat(actual.size(), is(2));
		assertThat(actual, hasItems(Money.HundredYen, Money.FiftyYen));
	}

	@Test
	public void 百円二枚と十円三枚あるところで120円の買い物をすると百円と十円二枚を使う() {
		List<Money> list = Arrays.asList(Money.HundredYen, Money.HundredYen, Money.TenYen, Money.TenYen, Money.TenYen);
		List<Money> actual = CreditService.getUseMoneyList(list, 120);
		assertThat(actual.size(), is(3));
		assertThat(actual, hasItems(Money.HundredYen, Money.TenYen, Money.TenYen));
	}

}
