package net.codersation.vendingmachine;

import static org.hamcrest.CoreMatchers.*;
import static org.junit.Assert.*;

import java.io.BufferedReader;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.io.PrintStream;
import java.io.StringReader;

import org.junit.Test;

public class MainTest {

	@Test
	public void お金の投入はinsでする() throws Exception {
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		System.setOut(new PrintStream(baos));

		byte[] input = "ins 100\nins 10\n".getBytes();
		System.setIn(new ByteArrayInputStream(input));

		Main.main();

		BufferedReader reader = new BufferedReader(new StringReader(baos.toString("UTF-8")));
		assertThat(reader.readLine(), is("> "));
		assertThat(reader.readLine(), is("money: 100 was received."));
		assertThat(reader.readLine(), is("> "));
		assertThat(reader.readLine(), is("money: 10 was received."));
	}

	@Test
	public void 変なお金をinsしたらば() throws Exception {
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		System.setOut(new PrintStream(baos));

		byte[] input = "ins 123".getBytes();
		System.setIn(new ByteArrayInputStream(input));

		Main.main();

		BufferedReader reader = new BufferedReader(new StringReader(baos.toString("UTF-8")));
		assertThat(reader.readLine(), is("> "));
		assertThat(reader.readLine(), is("! 123 is not available."));
	}
}
