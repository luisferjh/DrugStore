<template>
  <v-card>
    <v-card-title>
      <span class="headline">Nuevo Producto</span>
    </v-card-title>
    <v-card-text>
      <v-container>
        <v-row>
          <v-col cols="12" sm="6" md="12">
            <v-text-field label="Nombre del producto*" v-model="nameProduct" required></v-text-field>
          </v-col>
          <v-col cols="12" sm="6" md="5">
            <v-select
              v-model="category"
              :items="categories"
              item-text="name"
              item-value="idCategory"
              label="Categoria*"
              required
            ></v-select>                        
          </v-col>
          <v-col cols="12" sm="6" md="5">
            <v-select
              v-model="laboratory"
              :items="laboratories"
              item-text="laboratoryName"
              item-value="idLaboratory"
              label="Laboratorio*"
              required
            ></v-select>                         
          </v-col>
          <v-col cols="12" md="2">
            <v-text-field label="Stock*" v-model="stock" required></v-text-field>
          </v-col>

          <v-col cols="12" lg="6">
            <v-menu
              v-model="menu1"
              :close-on-content-click="false"
              full-width
              max-width="290"
            >
              <template v-slot:activator="{ on }">
                <v-text-field
                  v-model="dueDate"
                  clearable
                  prepend-icon="event"
                  label="Fecha de vencimiento"                              
                  v-on="on"
                ></v-text-field>
              </template>
              <v-date-picker
                v-model="dueDate"
                no-title
                @change="menu1 = false"
              ></v-date-picker>
            </v-menu>
          </v-col>

          <v-col cols="12">
            <v-text-field label="Codigo de barra*" v-model="barCode"></v-text-field>
          </v-col>                     

            <v-col cols="12">
            <v-text-field label="Indicativo*" v-model="indicative" ></v-text-field>
          </v-col>

          <v-col cols="12">
            <v-text-field label="Precio*" v-model="price" required></v-text-field>
          </v-col>
          <v-col cols="12">
            
            <v-select
              v-model="estado"
              :items="[{'estado':'Activo', 'value':true}, {'estado':'Inactivo', 'value':false}]"   
              item-text="estado"
              item-value="value"                       
              label="Estado*"
              required
            ></v-select>  
          </v-col>
        </v-row>
      </v-container>
      <small>*indica campo obligatorio</small>
      
    </v-card-text>
    <v-card-actions>
      <div class="flex-grow-1"></div>
      <v-btn color="blue darken-1" text @click="cancel()">Cerrar</v-btn>
      <v-btn color="blue darken-1" text @click="save()">Guardar</v-btn>
    </v-card-actions>
  </v-card> 
</template>

<script>
  export default {
    props: {          
      categories:{
        type:Array,
        required:true
      },
      laboratories:{
        type:Array,
        required:true
      }
    },
    data() {
      return {
        nameProduct:'',
        category:'',
        laboratory:'',
        stock:0,
        dueDate:'',
        barCode:'',
        indicative:'',
        price:'',
        estado:'',
        dueDate: new Date().toISOString().substr(0, 10),
        menu1: false
      }
    },
    created () {
      // this.productObj= Object.assign({}, this.product)  
    },
    methods: {
      save() {
        this.$emit('save',{ 
          'idCategory':this.category,
          'idLaboratory':this.laboratory,
          'productName':this.nameProduct,
          'stock':this.stock,
          'indicative':this.indicative,                        
          'barCode':this.barCode,
          'price':parseFloat(this.price),
          'condition':this.estado
        })
      },
      cancel(){
        this.$emit('cancel',false)
      }
    },
  }
</script>

