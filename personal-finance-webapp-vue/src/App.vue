<template>
  <div id="app">
    <v-app>
      <v-main>
        <v-container>
          <v-sheet :elevation="13">
            <h1>Personal Finance</h1>
            <Total />
            <router-view />
          </v-sheet>
        </v-container>
      </v-main>
    </v-app>
  </div>
</template>

<script>
import Total from '@/components/Total'
export default {
  name: 'App',
  components: {
    Total
  }
}
</script>

<style>
#app {
  background-color: #dbf2fb;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  margin-top: 60px;
}
</style>
