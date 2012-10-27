/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package net.codersation.vendingmachine;

import net.codersation.vendingmachine.MoneyBox;
import net.codersation.vendingmachine.Money;
import java.util.ArrayList;
import java.util.List;
import static org.hamcrest.CoreMatchers.*;
import org.junit.After;
import org.junit.AfterClass;
import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;

/**
 *
 * @author megascus
 */
public class MoneyBoxTest {
    
    public MoneyBoxTest() {
    }
    
    @BeforeClass
    public static void setUpClass() {
    }
    
    @AfterClass
    public static void tearDownClass() {
    }
    
    @Before
    public void setUp() {
    }
    
    @After
    public void tearDown() {
    }

    /**
     * Test of insert method, of class MoneyBox.
     */
    @Test
    public void amountIs0WhenFirst() {
        MoneyBox instance = new MoneyBox();
        assertThat(instance.getAmount(),is(0));
    }
    
    @Test
    public void amountIs150YenWhen50YenAnd100YenInserted() {
        MoneyBox instance = new MoneyBox();
        instance.insert(Money.ONE_HUNDRED);
        instance.insert(Money.FIFTY);
        assertThat(instance.getAmount(),is(150));
    }
    /**
     * Test of getAmount method, of class MoneyBox.
     */
    @Test
    public void getChangeReturnsInsertedMoneys() {
        MoneyBox instance = new MoneyBox();
        List<Money> moneys = new ArrayList<>();
        moneys.add(Money.TEN);
        moneys.add(Money.TEN);
        moneys.add(Money.FIVE_HUNDREDS);
        moneys.add(Money.ONE_THOUSAND);
        
        for(Money money: moneys) {
            instance.insert(money);
        }
        List<Money> result = instance.getChange();
        assertThat(moneys, is(result));
    }
    
    @Test
    public void getChangeReturnsAfterPay() {
        MoneyBox instance = new MoneyBox();
        List<Money> moneys = new ArrayList<>();
        moneys.add(Money.TEN);
        moneys.add(Money.TEN);
        moneys.add(Money.FIVE_HUNDREDS);
        moneys.add(Money.ONE_HUNDRED);
        
        for(Money money: moneys) {
            instance.insert(money);
        }
        assertTrue(instance.canPay(620));
        assertFalse(instance.canPay(621));
        instance.pay(111);
        List<Money> result = instance.getChange();
        List<Money> expected = new ArrayList<Money>();
        expected.add(Money.FIVE_HUNDREDS);
        expected.add(Money.FIVE);
        expected.add(Money.ONE);
        expected.add(Money.ONE);
        expected.add(Money.ONE);
        expected.add(Money.ONE);
        assertThat(result, is(expected));
    }

    /**
     * Test of getChange method, of class MoneyBox.
     */
    @Test
    public void clearChangesAmountAndInsertedMoneysToZero() {
        MoneyBox instance = new MoneyBox();
        List<Money> moneys = new ArrayList<>();
        moneys.add(Money.TEN);
        moneys.add(Money.TEN);
        moneys.add(Money.FIVE_HUNDREDS);
        moneys.add(Money.ONE_THOUSAND);
        
        for(Money money: moneys) {
            instance.insert(money);
        }
        assertThat(instance.getAmount(),is(not(0)));
        assertThat(instance.getChange().size(),is(not(0)));
        
        instance.clear();
        assertThat(instance.getAmount(),is(0));
        assertThat(instance.getChange().size(),is(0));
    }
}
