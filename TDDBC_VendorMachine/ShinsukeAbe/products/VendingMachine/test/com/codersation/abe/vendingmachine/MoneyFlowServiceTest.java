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
public class MoneyFlowServiceTest {
    
    public MoneyFlowServiceTest() {
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
    
    // TODO 投入金額200円で120円のコーラを購入すると80円のお釣りが返却される
    // TODO 等入金額200円で120円のコーラを購入すると投入金額合計は0円になる
    // TODO 等入金額120円で120円のコーラを購入すると0円のお釣りが返却される
    // TODO 在庫がない場合の購入動作 <= アプリケーション層
}
