<template>
  <div id="app">
    <v-app>
      <v-main>
        <v-container>
          <v-sheet :elevation="13">
            <router-view />
          </v-sheet>
        </v-container>
      </v-main>
    </v-app>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>

<style>
#app {
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  margin-top: 60px;
}
</style>
