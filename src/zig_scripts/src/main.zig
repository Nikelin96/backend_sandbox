const std = @import("std");
const hp = @import("./helper.zig");

pub fn main() !void {
    // const file_path = "D:/backend_sandbox/src/zig_scripts/src/input.txt";
    const file_path = "/home/mykola/workplace/backend_sandbox/src/zig_scripts/src/input.txt";
    const allocator = std.heap.page_allocator;
    const stdout = std.io.getStdOut().writer();

    const file = try std.fs.cwd().openFile(file_path, .{});
    defer file.close();

    var int_list = std.ArrayList(i32).init(allocator);
    defer int_list.deinit();

    var buf_reader = std.io.bufferedReader(file.reader());
    var in_stream = buf_reader.reader();
    var buf: [1024]u8 = undefined;
    var result: u16 = 0;
    while (try in_stream.readUntilDelimiterOrEof(&buf, '\n')) |line| {
        try stdout.print("Line: {s}\n", .{line});

        result += try hp.getTwoDigitNumber(line);
    }
    try stdout.print("Final result: {}\n", .{result});

    const stdin = std.io.getStdIn();
    var buffer: [10]u8 = undefined;
    _ = try stdin.read(buffer[0..]);
}

test "simple test" {
    var list = std.ArrayList(i32).init(std.testing.allocator);
    defer list.deinit(); // try commenting this out and see if zig detects the memory leak!
    try list.append(42);
    try std.testing.expectEqual(@as(i32, 42), list.pop());
}

const parseInt = std.fmt.parseInt;

test "parse integers" {
    const input = "123 sdfsdfsdf67 89,sdfsd99 sdcasd";
    const ally = std.testing.allocator;

    var list = std.ArrayList(u32).init(ally);
    // Ensure the list is freed at scope exit.
    // Try commenting out this line!
    defer list.deinit();

    var it = std.mem.tokenizeAny(u8, input, " ,");
    while (it.next()) |num| {
        const n = try parseInt(u32, num, 10);
        try list.append(n);
    }

    const expected = [_]u32{ 123, 67, 89, 99 };

    for (expected, list.items) |exp, actual| {
        try std.testing.expectEqual(exp, actual);
    }
}
