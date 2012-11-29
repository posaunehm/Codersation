package test.com.github.springaki.codersation;

import static org.junit.Assert.assertEquals;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import com.github.springaki.codersation.Money;

public class MoneyTest {

	@Before
	public void setUp() throws Exception {
	}

	@After
	public void tearDown() throws Exception {
	}

	@Test
	public void testToInt() {
		assertEquals(10, Money.Ten.toInt());
	}

}
