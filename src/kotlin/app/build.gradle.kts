import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    // Apply the org.jetbrains.kotlin.jvm Plugin to add support for Kotlin.
    kotlin("jvm") version "1.6.0"

    // Apply the application plugin to add support for building a CLI application in Java.
    application
}

group = "com.joewoess"
version = "1.0.0"
description = "adventofcode 2021 implementation"

java.sourceCompatibility = JavaVersion.VERSION_11

repositories {
    // Use JCenter for resolving dependencies.
    mavenCentral()
}

dependencies {
    // Align versions of all Kotlin components
    implementation(platform(kotlin("bom")))

    // Use the Kotlin reflect library.
    implementation(kotlin("reflect"))

    // Use the Kotlin JDK standard library.
    implementation(kotlin("stdlib"))
}

application {
    // Define the main class for the application.
    mainClass.set("adventofcode.AppKt")
}

sourceSets.main {
    java.srcDir("adventofcode")
}

tasks.withType<KotlinCompile> {
    kotlinOptions {
        freeCompilerArgs = listOf("-Xjsr305=strict")
        jvmTarget = "11"
    }
}
