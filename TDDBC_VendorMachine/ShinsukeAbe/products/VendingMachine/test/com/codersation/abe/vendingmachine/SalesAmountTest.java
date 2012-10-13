/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.codersation.abe.vendingmachine;

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
public class SalesAmountTest {
    
    private SalesAmount amount;
    
    public SalesAmountTest() {
    }
    
    @BeforeClass
    public static void setUpClass() {
    }
    
    @AfterClass
    public static void tearDownClass() {
    }
    
    @Before
    public void setUp() {
        amount = new SalesAmount();
    }
    
    @After
    public void tearDown() {
    }
    
    // TODO 購入動作 <= アプリケーション層の振る舞い
    
    @Test
    public void 初期状態で売上金額合計を取得すると0円である() {
        assertThat(amount.getTotal(), is(0));
    }
    
    @Test
    public void 売上に120円追加して売上金額合計を取得すると120円である() {
        amount.add(120);
        assertThat(amount.getTotal(), is(120));
    }
}
