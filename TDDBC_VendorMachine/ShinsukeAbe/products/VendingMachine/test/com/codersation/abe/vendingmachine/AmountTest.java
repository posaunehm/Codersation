package com.codersation.abe.vendingmachine;

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
    
    @Test
    public void 自販機に10円を投入すると投入金額合計が10円になる() {
        amount.insert(AmountableMoneyFactory.createNewMoney(10));
        assertThat(amount.getTotal(), is(10));
    }
    
    @Test
    public void 自販機に100円を投入すると投入金額合計が100円になる() {
        amount.insert(AmountableMoneyFactory.createNewMoney(100));
        assertThat(amount.getTotal(), is(100));
    }
    
    @Test
    public void 自販機に50円と500円を投入すると投入金額合計が550円になる() {
        amount.insert(AmountableMoneyFactory.createNewMoney(50));
        amount.insert(AmountableMoneyFactory.createNewMoney(500));
        assertThat(amount.getTotal(), is(550));
    }
    
    @Test
    public void 自販機に1000円と100円を投入して払い戻しを行うと投入金額合計が0円になる() {
        amount.insert(AmountableMoneyFactory.createNewMoney(1000));
        amount.insert(AmountableMoneyFactory.createNewMoney(100));
        amount.payBack();
        assertThat(amount.getTotal(), is(0));
    }
    
    @Test
    public void 自販機に1000円と100円を投入して払い戻しを行うと1100円戻ってくる() {
        amount.insert(AmountableMoneyFactory.createNewMoney(1000));
        amount.insert(AmountableMoneyFactory.createNewMoney(100));
        assertThat(amount.payBack(), is(1100));
    }
    
    @Test(expected = IllegalArgumentException.class)
    public void 自販機に1円を投入すると例外がスローされる() {
        AmountableMoneyFactory.createNewMoney(1);
    }
}
