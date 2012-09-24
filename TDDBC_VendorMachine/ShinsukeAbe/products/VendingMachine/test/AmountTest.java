/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

import com.codersation.abe.vendingmachine.money.AmountableMoneyFactory;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import static org.hamcrest.core.Is.*;

/**
 *
 * @author mao
 */
public class AmountTest {
    
    private UserAmount amount;
    
    public AmountTest() {
    }
    
    @BeforeClass
    public static void setUpClass() {
    }
    
    @AfterClass
    public static void tearDownClass() {
    }
    
    @Before
    public void setUp() {
        amount = new UserAmount();
    }
    
    @After
    public void tearDown() {
    }
    
    // TODO 自販機に1000円を投入すると投入金額合計が1000円になる
    // TODO 自販機に1000円と100円を投入して払い戻しを行うと投入金額合計が0円になる
    // TODO 自販機に1000円と100円を投入して払い戻しを行うと1100円戻ってくる
    
    @Test
    public void 自販機に10円を投入すると投入金額合計が10円になる() {
        amount.addAmount(AmountableMoneyFactory.createNewMoney(10));
        assertThat(amount.getTotal(), is(10));
    }
    
    @Test
    public void 自販機に100円を投入すると投入金額合計が100円になる() {
        amount.addAmount(AmountableMoneyFactory.createNewMoney(100));
        assertThat(amount.getTotal(), is(100));
    }
    
    @Test
    public void 自販機に50円と500円を投入すると投入金額合計が550円になる() {
        amount.addAmount(AmountableMoneyFactory.createNewMoney(50));
        amount.addAmount(AmountableMoneyFactory.createNewMoney(500));
        assertThat(amount.getTotal(), is(550));
    }
}
