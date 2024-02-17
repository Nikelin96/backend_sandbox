const std = @import("std");
const hp = @import("./helper.zig");

pub fn main() !void {
    std.debug.print("Hello, world!\n", .{});
    const a = 5;
    const b = 10;

    const result = hp.sum(a, b);

    std.debug.print("result: {}\n", .{result});
    const stdin = std.io.getStdIn();

    std.debug.print("Press Enter to exit...\n", .{});

    var buffer: [10]u8 = undefined;
    // var input: i64 = undefined;
    // if (try stdin.readUntilDelimiterOrEof(buf[0..], '\n')) |user_input| {
    //     input = std.fmt.parseInt(i64, user_input, 10);
    // } else {
    //     input = @as(i64, 0);
    // }
    // Read from stdin; this waits for the user to press Enter
    _ = try stdin.read(buffer[0..]);

    std.debug.print("Exiting.\n", .{});
}

// pub fn main() !void {
//     // Prints to stderr (it's a shortcut based on `std.io.getStdErr()`)
//     std.debug.print("All your {s} are belong to us.\n", .{"codebase"});

//     // stdout is for the actual output of your application, for example if you
//     // are implementing gzip, then only the compressed bytes should be sent to
//     // stdout, not any debugging messages.
//     const stdout_file = std.io.getStdOut().writer();
//     var bw = std.io.bufferedWriter(stdout_file);
//     const stdout = bw.writer();

//     try stdout.print("Run `zig build test` to run the tests.\n", .{});

//     try bw.flush(); // don't forget to flush!
// }

test "simple test" {
    var list = std.ArrayList(i32).init(std.testing.allocator);
    defer list.deinit(); // try commenting this out and see if zig detects the memory leak!
    try list.append(42);
    try std.testing.expectEqual(@as(i32, 42), list.pop());
}

const parseInt = std.fmt.parseInt;

test "parse integers" {
    const input = "123 67 89,99";
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

// // build with `zig build-exe cimport.zig -lc -lraylib`
// const ray = @cImport({
//     @cInclude("raylib.h");
// });

// pub fn main() void {
//     const screenWidth = 800;
//     const screenHeight = 450;

//     ray.InitWindow(screenWidth, screenHeight, "raylib [core] example - basic window");
//     defer ray.CloseWindow();

//     ray.SetTargetFPS(60);

//     while (!ray.WindowShouldClose()) {
//         ray.BeginDrawing();
//         defer ray.EndDrawing();

//         ray.ClearBackground(ray.RAYWHITE);
//         ray.DrawText("Hello, World!", 190, 200, 20, ray.LIGHTGRAY);
//     }
// }
