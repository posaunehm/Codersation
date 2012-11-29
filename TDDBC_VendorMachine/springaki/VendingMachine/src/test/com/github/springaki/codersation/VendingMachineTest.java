package test.com.github.springaki.codersation;

import static org.junit.Assert.assertEquals;

import org.junit.Test;

import com.github.springaki.codersation.Money;
import com.github.springaki.codersation.VendingMachine;

public class VendingMachineTest {

	VendingMachine sut = new VendingMachine();

	@Test
	public void testInsertMoney() {
		sut.insertMoney(Money.Ten);
	}

	@Test
	public void _10‰~‹Ê_50‰~‹Ê_100‰~‹Ê_500‰~‹Ê_1000‰~D‚ğ‚P‚Â‚¸‚Â“Š“ü‚Å‚«‚é() {
		sut.insertMoney(Money.Ten);
		sut.insertMoney(Money.Fifty);		
		sut.insertMoney(Money.OneHundred);		
		sut.insertMoney(Money.FiveHundred);		
		sut.insertMoney(Money.OneThousand);		
	}

	@Test
	public void “Š“ü‚Í•¡”‰ñ‚Å‚«‚é() {
		sut.insertMoney(Money.Ten);
		sut.insertMoney(Money.Ten);
	}

	@Test
	public void “Š“ü‹àŠz‚Ì‘Œv‚ğæ“¾‚Å‚«‚é() {
		assertEquals(0, sut.getTotalAmount());		
		sut.insertMoney(Money.Ten);
		sut.insertMoney(Money.Ten);
		assertEquals(20, sut.getTotalAmount());		
	}
	
	@Test
	public void •¥‚¢–ß‚µ‘€ì‚ğs‚¤‚Æ“Š“ü‹àŠz‚Ì‘Œv‚ğ’Ş‚è‘K‚Æ‚µ‚Äo—Í‚·‚é() {
		assertEquals(0, sut.calcel());
		sut.insertMoney(Money.Ten);
		sut.insertMoney(Money.Ten);
		assertEquals(20, sut.calcel());
		assertEquals(0, sut.calcel());
	}

}
